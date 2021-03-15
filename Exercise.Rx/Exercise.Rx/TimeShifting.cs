using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Rx
{
    [TestClass]
    public class TimeShifting
    {
        [TestMethod]
        public void Buffer()
        {
            var idealBatchSize = 15;

            var source = Observable.Range(0, 100);

            source.Buffer(idealBatchSize)
                .Subscribe(
                buffer => Console.WriteLine("Buffer of {1} @ {0}, sum:{2}", DateTime.Now, buffer.Count, buffer.Sum()),
                () => Console.WriteLine("Completed"));
        }

        [TestMethod]
        public void Sample()
        {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(150));

            interval.Sample(TimeSpan.FromMilliseconds(500))
                .Print("Sample");

            Thread.Sleep(1600);
        }
    }
}
