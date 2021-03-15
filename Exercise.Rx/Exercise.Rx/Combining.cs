using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Rx
{

    [TestClass]
    public class Combining
    {
        [TestMethod]
        public void Amb()
        {
            var s1 = new Subject<int>();
            var s2 = new Subject<int>();
            var s3 = new Subject<int>();

            Observable.Amb(s1, s2, s3).Print("Amb");

            s1.OnNext(1);
            s2.OnNext(2);
            s3.OnNext(3);
            s1.OnNext(1);
            s2.OnNext(2);
            s3.OnNext(3);
            s1.OnCompleted();
            s2.OnCompleted();
            s3.OnCompleted();
        }

        [TestMethod]
        public void Merge()
        {
            var s1 = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(3);

            var s2 = Observable.Interval(TimeSpan.FromMilliseconds(150))
                .Take(5)
                .Select(i => i + 100);

            s1.Merge(s2).Print("Merge");

            Thread.Sleep(1000);
        }
    }
}
