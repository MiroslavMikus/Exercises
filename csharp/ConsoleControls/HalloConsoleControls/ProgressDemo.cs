using System.Threading.Tasks;
using DustInTheWind.ConsoleTools.Spinners;

namespace HalloConsoleControls
{
    public class ProgressDemo : IDemo
    {
        public void Print()
        {
            var progressBar = new ProgressBar();
            var task = Task.Run(async () =>
            {
                progressBar.Display();

                for (var i = 0; i < 100; i++)
                {
                    await Task.Delay(50);
                    progressBar.Value++;
                }
            });

            task.Wait();

            progressBar.Close();
        }
    }
}