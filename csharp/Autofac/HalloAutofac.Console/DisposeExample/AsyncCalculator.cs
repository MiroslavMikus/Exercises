using System;
using System.Threading.Tasks;

namespace HalloAutofac.Console.DisposeExample
{
    public class AsyncCalculator : ICalculatorService, IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            await Task.Delay(500);
            System.Console.WriteLine("Disposed async :)");
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}