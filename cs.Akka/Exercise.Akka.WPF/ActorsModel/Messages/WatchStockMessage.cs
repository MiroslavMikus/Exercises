namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class WatchStockMessage
    {
        public string StockSymbol { get; }

        public WatchStockMessage(string stockSymbol)
        {
            StockSymbol = stockSymbol;
        }
    }
}
