using Akka.Actor;
using Exercise_Akka.WPF.ActorsModel.Messages;
using Exercise_Akka.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_Akka.WPF.ActorsModel.Actors
{
    public class StockPriceLookugActor : ReceiveActor
    {
        private readonly IStocPriceServiceGateway _stocPriceServiceGateway;

        public StockPriceLookugActor(IStocPriceServiceGateway stocPriceServiceGateway)
        {
            _stocPriceServiceGateway = stocPriceServiceGateway;

            Receive<RefreshStockPriceMessage>(message => LookupStockPrice(message));
        }

        private void LookupStockPrice(RefreshStockPriceMessage message)
        {
            var lastPrice = _stocPriceServiceGateway.GetLatestPrice(message.StockSymbol);

            Sender.Tell(new UpdatedStockPriceMessage(lastPrice, DateTime.Now));
        }
    }
}
