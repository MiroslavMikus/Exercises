using Akka.Actor;
using Akka.DI.Core;
using Exercise_Akka.WPF.ActorsModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka.WPF.ActorsModel.Actors
{
    public class StockActor : ReceiveActor
    {
        private readonly string _stockSymbol;
        private decimal _stockPrice;
        private readonly HashSet<IActorRef> _subscribers = new HashSet<IActorRef>();
        private readonly IActorRef _priceLookupChild;
        private ICancelable _priceRefreshing;

        public StockActor(string stockSymbol)
        {
            _stockSymbol = stockSymbol;

            _priceLookupChild = Context.ActorOf(Context.DI().Props<StockPriceLookugActor>());

            Receive<SubscribeToNewStockPriceMessage>(a => _subscribers.Add(a.Subscriber));
            Receive<UnSubscribeToNewStockPriceMessage>(a => _subscribers.Remove(a.Subscriber));

            Receive<RefreshStockPriceMessage>(a => _priceLookupChild.Tell(a));
            Receive<UpdatedStockPriceMessage>(a =>
            {
                _stockPrice = a.Price;

                var stockPriceMessage = new StockPriceMessage(_stockSymbol, _stockPrice, a.Date);

                foreach (var sub in _subscribers)
                {
                    sub.Tell(stockPriceMessage);
                }
            });
        }

        protected override void PreStart()
        {
            _priceRefreshing = Context.System
                .Scheduler
                .ScheduleTellRepeatedlyCancelable(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                Self,
                new RefreshStockPriceMessage(_stockSymbol),
                Self);
        }

        protected override void PostStop()
        {
            _priceRefreshing.Cancel(false);

            base.PostStop();
        }
    }
}
