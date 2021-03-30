using DustInTheWind.ConsoleTools.Menues;

namespace Exercise.ConsoleControls
{
    public class PrompterDemo : IDemo
    {
        public void Print()
        {
            var prompter = new Prompter();
            prompter.Display();
            var command = prompter.LastCommand;
        }
    }
}