using System;
using DustInTheWind.ConsoleTools;
using Exercise.ConsoleControls;

namespace HalloConsoleControls
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ShowDemo<PrompterDemo>();
            ShowDemo<TextMenuDemo>();
            ShowDemo<ScrollMenuDemo>();
            ShowDemo<SpinerDemo>();
            ShowDemo<ProgressDemo>();
            ShowDemo<YesNoDemo>();
            ShowDemo<TextDemo>();
            ShowDemo<DataGridDemo>();

            Console.WriteLine("The end");
            Console.ReadLine();
        }

        private static void ShowDemo<TDemo>() where TDemo : IDemo, new()
        {
            var textBlock = new TextBlock
            {
                Text = typeof(TDemo).Name,
                Margin = 1,
                ForegroundColor = ConsoleColor.Cyan
            };

            textBlock.Display();

            var demo = new TDemo();

            demo.Print();

            Console.WriteLine("Done - clearing console");
            Console.ReadLine();
            Console.Clear();
        }
    }
}