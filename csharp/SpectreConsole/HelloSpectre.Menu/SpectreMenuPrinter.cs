using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Spectre.Console;

namespace HelloSpectre.Menu
{
    public class SpectreMenuPrinter
    {
        private readonly string _header;
        public bool SkipNextReadline { get; set; } = false;
        private bool _repeat = true;
        public Action OnClose { get; set; } = () => { };

        public SpectreMenuPrinter(string header = null)
        {
            _header = header;
        }

        public async Task Print<T>(string title, string suffix = "Command")
        {
            while (_repeat)
            {
                Console.Clear();

                if (!string.IsNullOrEmpty(_header))
                {
                    AnsiConsole.Render(
                        new FigletText(_header)
                            .LeftAligned()
                            .Color(Color.Red));
                }

                var command = AnsiConsole.Prompt(new SelectionPrompt<MethodInfo>().Title($"[green]{title}[/]")
                    .AddChoices(GetCommands(typeof(T), suffix).OrderBy(a => a.Name))
                    .AddChoice(typeof(SpectreMenuPrinter).GetMethods()
                        .Single(a => a.Name == "Close"))
                    .UseConverter(a => a.Name.Replace(suffix, "")));

                AnsiConsole.MarkupLine($"[blue]{title}>{command.Name}[/]" + Environment.NewLine);
                if (!command.IsStatic) // closing
                {
                    command.Invoke(this, null);
                    OnClose();
                    continue;
                }

                try
                {
                    var result = command.Invoke(null, null);

                    if (result is Task resultTask)
                    {
                        await resultTask;
                    }
                }
                catch (Exception e)
                {
                    AnsiConsole.WriteException(e);
                }

                if (SkipNextReadline)
                {
                    SkipNextReadline = false;
                }
                else
                {
                    AnsiConsole.Markup(Environment.NewLine + $"Return to [underline red bold]{title}[/] menu");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private static MethodInfo[] GetCommands(Type assemblyType, string suffix)
        {
            return assemblyType.Assembly.GetTypes()
                .Where(a => a.IsAbstract && a.IsSealed)
                .SelectMany(a => a.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Where(a => a.Name.EndsWith(suffix))
                .ToArray();
        }

        public void Close() => _repeat = false;
    }
}