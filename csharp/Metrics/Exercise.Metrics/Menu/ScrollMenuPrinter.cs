using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Spectre.Console;

namespace Exercise.Metrics.Menu
{
    public class SpectreMenuPrinter
    {
        public static void Exit() => Environment.Exit(0);

        public async Task Print()
        {
            while (true)
            {
                var command = AnsiConsole.Prompt(new SelectionPrompt<MethodInfo>().Title("[green]Select command[/]")
                    .AddChoices(ReflectionTools.GetCommands(typeof(Program)).OrderBy(a => a.Name))
                    .AddChoice(typeof(SpectreMenuPrinter).GetMethods().Single(a => a.Name =="Exit"))
                    .UseConverter(a => a.Name.Replace("Command", "")));

                Console.Clear();
                
                var result = command.Invoke(null, null);

                if (result is Task resultTask)
                {
                    await resultTask;
                }
                
                AnsiConsole.Markup("[underline red]Return to menu[/]");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}