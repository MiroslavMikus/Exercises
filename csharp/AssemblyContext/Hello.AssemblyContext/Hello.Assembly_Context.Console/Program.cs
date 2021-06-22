using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Spectre.Console;

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

        public static void GetCurrentPluginsCommand()
        {
            var allTypes = AssemblyLoadContext.All
                .SelectMany(a => a.Assemblies)
                .SelectMany(a => a.GetTypes())
                .Where(FilterPlugin);

            PrintTypes(allTypes.ToArray());
        }

        public static void GetCurrentAndPluginsCommand()
        {
            var newContext = new AssemblyLoadContext(PluginsDir, true);

            foreach (var file in GetAllPluginDll().Where(a => !a.Contains("Core")))
            {
                System.Console.WriteLine($"Loading {file}");
                using var fs = File.Open(file, FileMode.Open);
                newContext.LoadFromStream(fs);
            }

            GetCurrentPluginsCommand();
        }

        public static void UnloadPluginsCommand()
        {
            var context = AssemblyLoadContext.All.FirstOrDefault(a => a.Name == PluginsDir);
            if (context == null)
            {
                System.Console.WriteLine("Context Plugins does not exist!");
            }
            else
            {
                System.Console.WriteLine("Unloading context");
                context.Unload();
            }
        }

        public static IEnumerable<string> GetAllPluginDll() => Directory.GetFiles(PluginsDir, "*.dll");
        public static bool FilterPlugin(Type t) => typeof(IPlugin).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract;

        public static void PrintTypes(IEnumerable<Type> types)
        {
            AnsiConsole.MarkupLine($"[red]All types[/]" + Environment.NewLine);

            System.Console.WriteLine(string.Join(Environment.NewLine, types));
        }
    }

    public class BasePlugin : IPlugin
    {
        public string Name { get; } = nameof(BasePlugin);
    }
}