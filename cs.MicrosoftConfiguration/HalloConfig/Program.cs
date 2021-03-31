using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using HalloConfig.Menu;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace HalloConfig
{
    public static class Program
    {
        public static IConfigurationRoot Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true)
            .AddEnvironmentVariables()
            .Build();

        static void Main(string[] args)
        {
            var menu = new ScrollMenuPrinter();
            menu.Print();
        }

        public static void ReadGroupValueCommand()
        {
            var group = Configuration.GetSection("SomeGroup");
            var value = group["SomeValue"];

            Console.WriteLine($"Value of section SomeGroup->SomeValue is {value}");
        }

        public static void GetDebugViewCommand()
        {
            var debug = Configuration.GetDebugView();

            Console.WriteLine(debug);
        }

        public static void ReadEnvironmentVariableCommand()
        {
            var appDataPath = Configuration["APPDATA"];

            Console.WriteLine(appDataPath);
        }

        public static void ShowConfigTreeCommand()
        {
            new TreeView(Configuration).WriteToConsole();
        }
    }
}