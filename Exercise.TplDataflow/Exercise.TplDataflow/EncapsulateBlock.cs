using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.TplDataflow
{
    [TestClass]
    public class EncapsulateBlock
    {
        [TestMethod]
        public async Task SimpleEncapsulatedBlock()
        {
            var increasingBlock = CreateFilterBlock<int>();

            var printBlock = new ActionBlock<int>(a => Console.WriteLine($"Print {a} received"));

            increasingBlock.LinkTo(printBlock, new DataflowLinkOptions { PropagateCompletion = true });

            await increasingBlock.SendAsync(1);
            await increasingBlock.SendAsync(2);
            await increasingBlock.SendAsync(1);
            await increasingBlock.SendAsync(3);
            await increasingBlock.SendAsync(4);
            await increasingBlock.SendAsync(2);
            await increasingBlock.SendAsync(5);
            await increasingBlock.SendAsync(3);
            await increasingBlock.SendAsync(6);

            increasingBlock.Complete();

            await printBlock.Completion;

            Console.WriteLine("Done!");
        }

        public static IPropagatorBlock<T, T> CreateFilterBlock<T>()
            where T : IComparable<T>, new()
        {
            T maxElement = default(T);

            var source = new BufferBlock<T>();
            var target = new ActionBlock<T>(async item =>
            {
                if (item.CompareTo(maxElement) > 0)
                {
                    await source.SendAsync(item);
                    maxElement = item;
                }
            });

            target.Completion.ContinueWith(a => 
            {
                if (a.IsFaulted)
                {
                    ((ITargetBlock<T>)source).Fault(a.Exception);
                }
                else
                {
                    source.Complete();
                }
            });

            return DataflowBlock.Encapsulate(target, source);
        }
    }
}
