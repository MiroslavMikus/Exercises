using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class ActionTest
    {
        [TestMethod]
        public async Task SimpleAction()
        {
            var block = new ActionBlock<int>(n =>
            {
                Console.WriteLine(n);
            });

            for (int i = 0; i < 10; i++)
            {
                block.Post(i);
            }

            block.Complete();

            Console.WriteLine("Done!");

            await block.Completion;
        }

        [TestMethod]
        public async Task ActionWithDelay()
        {
            var block = new ActionBlock<int>(n =>
            {
                Task.Delay(500).Wait();
                Console.WriteLine(n);
            });

            for (int i = 0; i < 10; i++)
            {
                block.Post(i);
                Console.WriteLine($"Action queue: {block.InputCount}");
            }

            block.Complete();

            Console.WriteLine("Done!");

            await block.Completion;
        }
    }
}
