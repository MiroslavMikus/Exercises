namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class UnWatchStockMessage
    {
        public string StockSymbol { get; }

        public UnWatchStockMessage(string stockSymbol)
        {
            StockSymbol = stockSymbol;
        }
    }
}
