using System;

namespace Exercise_Akka.WPF.Services
{
    public class RandomStockPriceServiceGateway : IStocPriceServiceGateway
    {
        private decimal _lastRandonPrice = 20;
        private readonly Random _random = new Random();

        public decimal GetLatestPrice(string stockSymbol)
        {
            var newPrice = _lastRandonPrice + _random.Next(-5, 5);

            if (newPrice < 0)
            {
                newPrice = 5;
            }

            if (newPrice > 50)
            {
                newPrice = 45;
            }

            return _lastRandonPrice = newPrice;
        }
    }
}
