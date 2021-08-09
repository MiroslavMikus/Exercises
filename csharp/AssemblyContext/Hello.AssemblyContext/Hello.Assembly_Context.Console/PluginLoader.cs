using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using Spectre.Console;

namespace Hello.Assembly_Context.Console
{
    public static class PluginLoader
    {
        public static void  GetCurrentPluginsCommand()
        {
            PrintTypes(GetCurrentPlugins().ToArray());
        }
        
        public static IEnumerable<Type> GetCurrentPlugins()
        {
            return AssemblyLoadContext.All
                .SelectMany(a => a.Assemblies)
                .SelectMany(a => a.GetTypes())
                .Where(FilterPlugin);
        }

        public static void GetCurrentAndPluginsCommand()
        {
            var newContext = new AssemblyLoadContext(Program.PluginsDir, true);

            foreach (var file in GetAllPluginDll().Where(a => !a.Contains("Core")))
            {
                System.Console.WriteLine($"Loading {file}");
                using var fs = File.Open(file, FileMode.Open);
                newContext.LoadFromStream(fs);
            }

            GetCurrentPluginsCommand();
        }

        public static void CreateAllPluginsCommand()
        {
            AnsiConsole.MarkupLine($"[red]Printing names:[/]" + Environment.NewLine);

            foreach (var plugin in GetCurrentPlugins())
            {
                var pluginInstance = Activator.CreateInstance(plugin) as IPlugin;
                System.Console.WriteLine(pluginInstance?.Name ?? $"Could not call name on {plugin.Name}");
            }
        }

        public static void UnloadPluginsCommand()
        {
            var context = AssemblyLoadContext.All.FirstOrDefault(a => a.Name == Program.PluginsDir);
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

        public static IEnumerable<string> GetAllPluginDll() => Directory.GetFiles(Program.PluginsDir, "*.dll");
        public static bool FilterPlugin(Type t) => typeof(IPlugin).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract;

        public static void PrintTypes(IEnumerable<Type> types)
        {
            AnsiConsole.MarkupLine($"[red]All types[/]" + Environment.NewLine);

            System.Console.WriteLine(string.Join(Environment.NewLine, types));
        }
        
    }
}