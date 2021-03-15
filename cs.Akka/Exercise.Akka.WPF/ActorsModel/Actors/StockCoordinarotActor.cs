using Akka.Actor;
using Exercise_Akka.WPF.ActorsModel.Messages;
using System.Collections.Generic;

namespace Exercise_Akka.WPF.ActorsModel.Actors
{
    public class StockCoordinarotActor : ReceiveActor
    {
        private readonly IActorRef _chartingActor;
        private readonly Dictionary<string, IActorRef> _stockActors = new Dictionary<string, IActorRef>();

        public StockCoordinarotActor(IActorRef chartingActor)
        {
            _chartingActor = chartingActor;

            Receive<WatchStockMessage>(a => WatchStock(a));
            Receive<UnWatchStockMessage>(a => UnWatchStock(a));
        }

        private void WatchStock(WatchStockMessage a)
        {
            bool childActorNeedsCreating = !_stockActors.ContainsKey(a.StockSymbol);

            if (childActorNeedsCreating)
            {
                var newChildActor = Context.ActorOf(Props.Create(() => new StockActor(a.StockSymbol)), "StockActor_" + a.StockSymbol);
                _stockActors.Add(a.StockSymbol, newChildActor);
            }

            _chartingActor.Tell(new AddChartSeriesMessage(a.StockSymbol));

            _stockActors[a.StockSymbol].Tell(new SubscribeToNewStockPriceMessage(_chartingActor));
        }

        private void UnWatchStock(UnWatchStockMessage a)
        {
            if (!_stockActors.ContainsKey(a.StockSymbol))
            {
                return;
            }

            _chartingActor.Tell(new RemoveChartSeriesMessage(a.StockSymbol));

            _stockActors[a.StockSymbol].Tell(new UnSubscribeToNewStockPriceMessage(_chartingActor));
        }
    }
}
