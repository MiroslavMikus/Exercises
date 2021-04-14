using System;
using System.Threading.Tasks;
using Spectre.Console;

namespace HelloSpectre
{
    public static class Demos
    {
        public static async Task ProgressBarDemo()
        {
            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    // Define tasks
                    var task1 = ctx.AddTask("[green]Reticulating splines[/]");
                    var task2 = ctx.AddTask("[green]Folding space[/]");

                    while (!ctx.IsFinished)
                    {
                        await Task.Delay(10);
                        task1.Increment(1.5);
                        task2.Increment(0.5);
                    }
                });
        }

        public static async Task StatusDemo()
        {
            await AnsiConsole.Status()
                .StartAsync("Thinking...", async ctx =>
                {
                    await Console.Out.WriteLineAsync("Wait for 2 sec...");
                    await Task.Delay(2_000);
                });
        }

        public static void PromptDemo()
        {
            var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");

            var age = AnsiConsole.Ask<int>("What's your [green]age[/]?");
            
            AnsiConsole.MarkupLine($"Your name is [green]{name}[/] and your age is [green]{age}[/].");
        }

        public static void TableDemo()
        {
            var table = new Table();

            table.AddColumn("Foo");
            table.AddColumn(new TableColumn("Bar").Centered());

            table.AddRow("Baz", "[green]Qux[/]");
            table.AddRow(new Markup("[blue]Corgi[/]"), new Panel("Waldo"));
            
            table.Border(TableBorder.Rounded);
            table.Expand();
            AnsiConsole.Render(table);
        }

        public static void TreeDemo()
        {
            var root = new Tree("Root");

            var foo = root.AddNode("[yellow]Foo[/]");
            var table = foo.AddNode(new Table()
                .RoundedBorder()
                .AddColumn("First")
                .AddColumn("Second")
                .AddRow("1", "2")
                .AddRow("3", "4")
                .AddRow("5", "6"));

            table.AddNode("[blue]Baz[/]");
            foo.AddNode("Qux");

            var bar = root.AddNode("[yellow]Bar[/]");
            bar.AddNode(new Calendar(2020, 12)
                .AddCalendarEvent(2020, 12, 12)
                .HideHeader());

            AnsiConsole.Render(root);
        }

        public static void ChartDemo()
        {
            AnsiConsole.Render(new BarChart()
                .Width(60)
                .Label("[green bold underline]Number of fruits[/]")
                .CenterLabel()
                .AddItem("Apple", 12, Color.Yellow)
                .AddItem("Orange", 54, Color.Green)
                .AddItem("Banana", 33, Color.Red));
        }
    }
}