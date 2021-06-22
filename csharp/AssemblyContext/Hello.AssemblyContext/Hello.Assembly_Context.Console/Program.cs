using System.IO;
using System.Threading.Tasks;

namespace Hello.Assembly_Context.Console
{
    public static class Program
    {
        public const string PluginsDir = "Plugin";

        static async Task Main(string[] args)
        {
            var menu = new SpectreMenuPrinter("AssemblyContext");
            await menu.Print<SpectreMenuPrinter>("Select command");
            Directory.CreateDirectory(PluginsDir);
        }

     
    }
}