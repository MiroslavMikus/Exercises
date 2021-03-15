using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class BufferTest
    {
        [TestMethod]
        public void SimpleBuffer()
        {
            var block = new BufferBlock<int>();

            for (int i = 0; i < 10; i++)
            {
                block.Post(i);
            }

            for (int i = 0; i < 10; i++)
            {
                var result = block.Receive();
                Console.WriteLine(result);
            }

            Console.WriteLine("Done!");
        }

        [TestMethod]
        public async Task Produce_Consumer()
        {
            var block = new BufferBlock<int>(new DataflowBlockOptions { BoundedCapacity = 1 });

            var action1 = new ActionBlock<int>(a =>
            {
                Console.WriteLine($"Processed by consumer 1: {a}");
                Task.Delay(300).Wait();
            }, new ExecutionDataflowBlockOptions() { BoundedCapacity = 1 });

            var action2 = new ActionBlock<int>(a =>
            {
                Console.WriteLine($"Processed by consumer 2: {a}");
                Task.Delay(300).Wait();
            }, new ExecutionDataflowBlockOptions() { BoundedCapacity = 1 });

            block.LinkTo(action1);
            block.LinkTo(action2);

            for (int i = 0; i < 10; i++)
            {
                await block.SendAsync(i);
            }

            block.Complete();

            await block.Completion;

            Console.WriteLine("Done!");
        }
    }
}
