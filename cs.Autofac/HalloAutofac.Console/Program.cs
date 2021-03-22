using System.Threading.Tasks;
using HalloAutofac.Console.DisposeExample;

namespace HalloAutofac.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Dispose_Example.Test();
        }
    }
}