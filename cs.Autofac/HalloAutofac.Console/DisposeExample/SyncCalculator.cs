using System;

namespace HalloAutofac.Console.DisposeExample
{
    public class SyncCalculator : ICalculatorService, IDisposable
    {
        public int Add(int a, int b)
        {
            return 55;
        }

        public void Dispose()
        {
            System.Console.WriteLine("Sync disposable");
        }
    }
}