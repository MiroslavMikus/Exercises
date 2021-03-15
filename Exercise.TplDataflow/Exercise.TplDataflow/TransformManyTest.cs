using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class TransformManyTest
    {
        [TestMethod]
        public async Task SimpleTransofmMany()
        {
            var block = new TransformManyBlock<int, string>(a => FindEven(a), 
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 5 });
            var printBlock = new ActionBlock<string>(a => Console.WriteLine($"Received: {a}"));
            block.LinkTo(printBlock);

            for (int i = 0; i < 10; i++)
            {
                block.Post(i);
            }

            Console.WriteLine("Done!");

            block.Complete();

            await block.Completion;
        }

        private static IEnumerable<string> FindEven(int nr)
        {
            for (int i = 0; i < nr; i++)
            {
                if (i % 2 == 0)
                {
                    yield return $"{nr}:{i}";
                }
            }
        }
    }
}
