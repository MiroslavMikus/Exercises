using DustInTheWind.ConsoleTools.CommandLine;
using DustInTheWind.ConsoleTools.Menues;

namespace Exercise.ConsoleControls
{
    public class PrompterDemo : IDemo
    {
        public void Print()
        {
            Prompter prompter = new Prompter();
            prompter.Display();
            CliCommand command = prompter.LastCommand;
        }
    }
}
