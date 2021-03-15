namespace Exercise_Akka.WPF.Services
{
    public interface IStocPriceServiceGateway
    {
        decimal GetLatestPrice(string stockSymbol);
    }
}
