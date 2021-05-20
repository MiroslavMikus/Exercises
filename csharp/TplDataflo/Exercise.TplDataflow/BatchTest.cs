using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class BatchTest
    {
        [TestMethod]
        public void SimpleBatch()
        {
            var block = new BatchBlock<int>(3);

            for (int i = 0; i < 10; i++)
            {
                block.Post(i);
            }

            block.Complete();

            for (int i = 0; i < 5; i++)
            {
                if (block.TryReceive(out int[] result))
                {
                    var output = result?.Select(a => a.ToString()).Aggregate((a, b) => $"{a} {b}");

                    Console.WriteLine($"Received {i}: {output}");
                }
            }

            Console.WriteLine("Done!");
        }
    }
}
