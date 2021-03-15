namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class RemoveChartSeriesMessage
    {
        public string StockSymbol { get; }

        public RemoveChartSeriesMessage(string stockSymbol)
        {
            StockSymbol = stockSymbol;
        }
    }
}
