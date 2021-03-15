using System.Threading.Tasks;

namespace Exercise.ConsoleControls
{
    public class ProgressDemo : IDemo
    {
        public void Print()
        {
            DustInTheWind.ConsoleTools.Spinners.ProgressBar progressBar = new DustInTheWind.ConsoleTools.Spinners.ProgressBar();
            var task = Task.Run(async () =>
            {
                progressBar.Display();

                for (int i = 0; i < 100; i++)
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
