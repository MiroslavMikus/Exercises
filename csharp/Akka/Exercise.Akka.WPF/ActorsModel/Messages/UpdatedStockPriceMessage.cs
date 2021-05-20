using System;

namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class UpdatedStockPriceMessage
    {
        public decimal Price { get; }
        public DateTime Date { get; }

        public UpdatedStockPriceMessage(decimal price, DateTime date)
        {
            Price = price;
            Date = date;
        }
    }
}
