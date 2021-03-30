using System;
using System.Threading.Tasks;
using DustInTheWind.ConsoleTools.Spinners;

namespace Exercise.ConsoleControls
{
    public class SpinerDemo : IDemo
    {
        public void Print()
        {
            Console.WriteLine("Just spin for 5 sec");

            Spinner.Run(() => { Task.Delay(5_000).Wait(); });
        }
    }
}