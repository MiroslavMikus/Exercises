using System;
using System.Linq;
using System.Reflection;
using Spectre.Console;

namespace HalloConfig.Menu
{
    public class SpectreMenuPrinter
    {
        public static void Exit() => Environment.Exit(0);

        public void Print()
        {
            while (true)
            {
                var command = AnsiConsole.Prompt(new SelectionPrompt<MethodInfo>().Title("[green]Select command[/]")
                    .AddChoices(ReflectionTools.GetCommands(typeof(Program)).OrderBy(a => a.Name))
                    .AddChoice(typeof(SpectreMenuPrinter).GetMethods().Single(a => a.Name =="Exit"))
                    .UseConverter(a => a.Name.Replace("Command", "")));

                Console.Clear();
                command.Invoke(null, null);
                AnsiConsole.Markup("[underline red]Return to menu[/]");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}