using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class WriteOnceBlockTest
    {
        [TestMethod]
        public void SimpleWriteOnce()
        {
            var block = new WriteOnceBlock<int>(a => a);

            for (int i = 0; i < 10; i++)
            {
                if (block.Post(i))
                {
                    Console.WriteLine($"Acceped {i}");
                }
                else
                {
                    Console.WriteLine($"Rejected {i}");
                }
            }

            for (int i = 0; i < 15; i++)
            {
                if (block.TryReceive(out var val))
                {
                    Console.WriteLine($"Received {val}");
                }
                else
                {
                    Console.WriteLine($"No more messages!");
                }
            }

            Console.WriteLine("Done!");
        }

        [TestMethod]
        public async Task WriteOnceWithAction()
        {
            var block = new WriteOnceBlock<int>(a => a);
            var print = new ActionBlock<int>(a => Console.WriteLine($"Message: {a}"));

            block.LinkTo(print);

            for (int i = 0; i < 10; i++)
            {
                if (block.Post(i))
                {
                    Console.WriteLine($"Acceped {i}");
                }
                else
                {
                    Console.WriteLine($"Rejected {i}");
                }
            }

            block.Complete();
            await block.Completion;
            print.Complete();
            await print.Completion;

            Console.WriteLine("Done!");
        }
    }
}
