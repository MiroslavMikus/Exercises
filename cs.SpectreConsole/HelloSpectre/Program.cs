using System.Diagnostics;
using System.Threading.Tasks;
using HelloSpectre.Menu;

namespace HelloSpectre
{
    class Program
    {
        public static SpectreMenuPrinter MainMenu { get; private set; }

        static async Task Main(string[] args)
        {
            MainMenu = new SpectreMenuPrinter("Spectre demo");
            await MainMenu.Print<Program>("Menu");
        }
    }
}