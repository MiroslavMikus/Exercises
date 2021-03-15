using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class ErrorHandlingTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task SimpleException()
        {
            var block = new ActionBlock<int>(n =>
            {
                if (n == 5)
                {
                    throw new Exception();
                }

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
        [ExpectedException(typeof(Exception))]
        public async Task DataFlowException()
        {
            var block = new TransformBlock<int, string>(n =>
            {
                if (n == 5)
                {
                    throw new Exception();
                }

                return n.ToString();
            });

            var printBlock = new ActionBlock<string>(n => Console.WriteLine($"Print {n}"));

            block.LinkTo(printBlock, new DataflowLinkOptions
            {
                PropagateCompletion = true
            });

            for (int i = 0; i < 10; i++)
            {
                if (await block.SendAsync(i))
                {
                    Console.WriteLine("Accepting: " + i);
                }
                else
                {
                    Console.WriteLine("Rejecting: " + i);
                }
            }
            await Task.Delay(200);

            block.Complete();

            await Task.Delay(200);

            Console.WriteLine("Done!");

            try
            {
                await printBlock.Completion;
            }
            catch (AggregateException ex)
            {
                throw ex.Flatten().InnerException;
            }
        }
    }
}
