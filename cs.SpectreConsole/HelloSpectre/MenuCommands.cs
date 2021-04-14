using System;
using System.Threading.Tasks;
using HelloSpectre.Menu;

namespace HelloSpectre
{
    public static class MenuCommands
    {
        public static void ThrowException()
        {
            throw new Exception("Some ex :)");
        }

        public static void SyncCommand()
        {
            Console.WriteLine("SomeCommand");
        }

        public static async Task AsyncCommand()
        {
            await Console.Out.WriteLineAsync("Some Write line async");
        }

        public static async Task OpenDemosCommand()
        {
            var demoMenu = new SpectreMenuPrinter() {OnClose = () => Program.MainMenu.SkipNextReadline = true};
            await demoMenu.Print<Program>("Main>Select demo", "Demo");
        }
    }
}