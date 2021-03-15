using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class JoinTest
    {
        [TestMethod]
        public async Task SimpleJoin()
        {
            var block = new BroadcastBlock<int>(a => a);

            Random random = new Random();

            var action1 = new TransformBlock<int, int>(a =>
            {
                Console.WriteLine($"Processed by consumer 1 : {a}");
                Task.Delay(random.Next(0, 1000)).Wait();
                return a * -1;
            }, new ExecutionDataflowBlockOptions());

            var action2 = new TransformBlock<int, int>(a =>
            {
                Console.WriteLine($"Processed by consumer 2 : {a}");
                Task.Delay(random.Next(0, 1000)).Wait();
                return a;
            }, new ExecutionDataflowBlockOptions());

            block.LinkTo(action1);
            block.LinkTo(action2);

            var joinBlock = new JoinBlock<int, int>();

            action1.LinkTo(joinBlock.Target1);
            action2.LinkTo(joinBlock.Target2);

            var printBlock = new ActionBlock<Tuple<int, int>>(a => Console.WriteLine($"Processed: {a}, Sum {a.Item1 + a.Item2}"));

            joinBlock.LinkTo(printBlock);

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
