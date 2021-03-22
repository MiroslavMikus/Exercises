using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace HalloAutofac.Console.DisposeExample
{
    public static class Dispose_Example
    {
        public static async Task Test()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AsyncCalculator>().As<ICalculatorService>();
            containerBuilder.RegisterType<SyncCalculator>().As<ICalculatorService>();

            var scope = containerBuilder.Build();

            var calcs = scope.Resolve<IEnumerable<ICalculatorService>>();

            var result = calcs.Select(a => a.Add(1, 2)).Sum();

            await scope.DisposeAsync();
        }
    }
}