using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class BroadcastTest
    {
        [TestMethod]
        public async Task SimpleBroadcast()
        {
            var block = new BroadcastBlock<int>(a => a);

            var action1 = new ActionBlock<int>(a =>
            {
                Console.WriteLine($"Processed by consumer 1 : {a}");
                Task.Delay(300).Wait();
            }, new ExecutionDataflowBlockOptions());

            var action2 = new ActionBlock<int>(a =>
            {
                Console.WriteLine($"Processed by consumer 2 : {a}");
                Task.Delay(300).Wait();
            }, new ExecutionDataflowBlockOptions());

            block.LinkTo(action1);
            block.LinkTo(action2);

            for (int i = 0; i < 10; i++)
            {
                await block.SendAsync(i);
                Console.WriteLine($"Message {i} was sended");
            }

            block.Complete();

            await block.Completion;

            action2.Complete();
            action1.Complete();

            await action2.Completion;
            await action1.Completion;

            Console.WriteLine("Done!");
        }
    }
}
