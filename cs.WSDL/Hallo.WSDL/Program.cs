using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Hallo.WSDL.Menu;
using Hallo.WSDL.TestService;
using Spectre.Console;

namespace Hallo.WSDL
{
    public static class Program
    {
        private static TempConvertSoapClient _client;

        static async Task Main(string[] args)
        {
            var binding = new BasicHttpBinding
            {
                MaxReceivedMessageSize = 1_000_000,
            };
            
            var endpoint = new EndpointAddress("http://www.w3schools.com/xml/tempconvert.asmx");

            // var se = new ServiceEndpoint(new ContractDescription("TempConvertSoap"));

            _client = new TestService.TempConvertSoapClient(binding, endpoint);
            
            var menu = new SpectreMenuPrinter();
            await menu.Print();
        }

        public static void FahrenheitToCelsiusCommand()
        {
            var fahrenheit = AnsiConsole.Ask<int>("Enter [green]fahrenheit[/] value:");

            var result = _client.FahrenheitToCelsius(fahrenheit.ToString());

            AnsiConsole.WriteLine("Your result is [green]{0}°C", result);

            Console.ReadLine();
        }
    }
}