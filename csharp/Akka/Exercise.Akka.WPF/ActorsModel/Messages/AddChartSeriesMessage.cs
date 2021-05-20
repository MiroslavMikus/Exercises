namespace Exercise_Akka.WPF.ActorsModel.Messages
{
    public class AddChartSeriesMessage
    {
        public string StockSymbol { get; }

        public AddChartSeriesMessage(string stockSymbol)
        {
            StockSymbol = stockSymbol;
        }
    }
}
