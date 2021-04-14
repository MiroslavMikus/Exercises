using System;
using Spectre.Console;
using Spectre.Console.Cli;

namespace HelloSpectre.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandApp();

            app.Configure(config =>
            {
                config.AddBranch<PrintSettings>("print", add =>
                {
                    add.AddCommand<AddPackageCommand>("package");
                    add.AddCommand<PrintTitleCommand>("title");
                });
            });

            return app.Run(args);
        }
    }
    
    public class AddPackageCommand : Command<PrintPackageSettings>
    {
        public override int Execute(CommandContext context, PrintPackageSettings settings)
        {
            Console.WriteLine(settings.Version);
            Console.WriteLine(settings.PackageName);
            // Omitted
            return 1;
        }
    }

    public class PrintTitleCommand : Command<PrintTitleSettings>
    {
        public override int Execute(CommandContext context, PrintTitleSettings settings)
        {
            AnsiConsole.Render(
                new FigletText(settings.Title)
                    .LeftAligned()
                    .Color(Color.Red));
            return 0;
        }
    }

    public class PrintSettings : CommandSettings
    {
        [CommandArgument(0, "[PROJECT]")]
        public string Project { get; set; }
    }

    public class PrintPackageSettings : PrintSettings
    {
        [CommandArgument(0, "<PACKAGE_NAME>")]
        public string PackageName { get; set; }

        [CommandOption("-v|--version <VERSION>")]
        public string Version { get; set; }
    }

    public class PrintTitleSettings : PrintSettings
    {
        [CommandArgument(0, "<Title_to_print>")]
        public string Title { get; set; }
    }
}