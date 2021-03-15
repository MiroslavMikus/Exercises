using DustInTheWind.ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Exercise.ConsoleControls
{
    class Program
    {
        static void Main(string[] args)
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

        public static void ShowDemo<TDemo>() where TDemo : IDemo, new()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = typeof(TDemo).Name,
                MarginTop = 1,
                MarginBottom = 1,
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
