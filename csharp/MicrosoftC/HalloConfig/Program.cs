using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Threading;
using HalloConfig.ConfigDebouncer;
using HalloConfig.Menu;
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
            var menu = new SpectreMenuPrinter();
            menu.Print();
        }

        public static void WaitForValueChangeCommand()
        {
            var autoReset = new AutoResetEvent(false);

            string GetValue() => Configuration.GetSection("SomeGroup")["SomeValue"];

            AnsiConsole.MarkupLine($"Waiting for section update [aqua]SomeGroup->SomeValue[/] (1 Min) with section delay 1 sec");

            using var token = Configuration.OnChange(() =>
            {
                AnsiConsole.MarkupLine(
                    $"Value of section [red]changed[/] [aqua]SomeGroup->SomeValue[/] is [purple]{GetValue()}[/]");
            }, TimeSpan.FromSeconds(1));

            autoReset.WaitOne(TimeSpan.FromMinutes(1));

            AnsiConsole.MarkupLine($"Value of section [aqua]SomeGroup->SomeValue[/] is [purple]{GetValue()}[/]");
        }

        public static void ReadGroupValueCommand()
        {
            var group = Configuration.GetSection("SomeGroup");
            var value = group["SomeValue"];
            AnsiConsole.MarkupLine($"Value of section [aqua]SomeGroup->SomeValue[/] is [purple]{value}[/]");
        }

        public static void GetDebugViewCommand()
        {
            var debug = Configuration.GetDebugView();

            Console.WriteLine(debug);
        }

        public static void ReadEnvironmentVariableCommand()
        {
            AnsiConsole.MarkupLine($"[green]APPDATA[/] value is [aqua]{Configuration["APPDATA"]}[/]");
        }

        public static void ShowConfigTreeCommand()
        {
            new TreeView(Configuration).WriteToConsole();
        }
    }
}