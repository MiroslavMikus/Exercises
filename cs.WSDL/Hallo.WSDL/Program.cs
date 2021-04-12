using System;
using System.Threading.Tasks;
using Hallo.WSDL.Menu;
using Hallo.WSDL.TestService;
using Spectre.Console;

namespace Hallo.WSDL
{
    class Program
    {
        private static TempConvertSoapClient _client;

        static async Task Main(string[] args)
        {
            _client = new TestService.TempConvertSoapClient();
            
            var menu = new SpectreMenuPrinter();
            await menu.Print();
        }

        public static void FahrenheitToCelsius()
        {
            var fahrenheit = AnsiConsole.Ask<int>("Enter [green]fahrenheit[/] value:");

            var result = _client.FahrenheitToCelsius(fahrenheit.ToString());
            
            AnsiConsole.WriteLine("Your result is [green]{0}°C", result);

            Console.ReadLine();
        }
    }
}
