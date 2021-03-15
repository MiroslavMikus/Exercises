using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Rx
{
    [TestClass]
    public class Aggregations
    {
        [TestMethod]
        public void Count()
        {
            var numbers = Observable.Range(0, 3);

            numbers.Subscribe(Console.WriteLine);

            numbers.Count().Subscribe(a => Console.WriteLine($"Count {a}"));
        }

        [TestMethod]
        public void Aggregate()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            subject.Aggregate((acc, currentValue) => acc + currentValue)
                .Print("Aggregate");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);

            subject.OnCompleted();
        }

        [TestMethod]
        public void Scan()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            subject.Scan((acc, currentValue) => acc + currentValue)
                .Print("Scan");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);

            subject.OnCompleted();
        }

        [TestMethod]
        public void GroupBy()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            subject.GroupBy(i => i % 3)
                .SelectMany(
                    grp =>
                    grp.Min()
                        .Select(value => new { grp.Key, value }))
                        .Print("GroupBy");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);

            subject.OnCompleted();
        }
    }
}
