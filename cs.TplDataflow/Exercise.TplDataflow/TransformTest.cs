using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class TransformTest
    {
        [TestMethod]
        public void SimpleTransform()
        {
            var block = new TransformBlock<int, string>(a =>
            {
                Task.Delay(500).Wait();
                return a.ToString();
            });

            for (int i = 0; i < 10; i++)
            {
                block.Post(i);
                Console.WriteLine($"Transform input queue: {block.InputCount}");
            }

            for (int i = 0; i < 10; i++)
            {
                var result = block.Receive();
                Console.WriteLine($"Received: {result}");
                Console.WriteLine($"Transform output queue: {block.OutputCount}");
            }

            Console.WriteLine("Done!");
        }

        [TestMethod]
        public void TransformParallel()
        {
            var block = new TransformBlock<int, string>(a =>
            {
                Task.Delay(500).Wait();
                return a.ToString();
            }, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 10 });

            for (int i = 0; i < 100; i++)
            {
                block.Post(i);
                Console.WriteLine($"Transform input queue: {block.InputCount}");
            }

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Transform output queue: {block.OutputCount}");
                var result = block.Receive();
                Console.WriteLine($"Received: {result}");
            }

            Console.WriteLine("Done!");
        }
    }
}
