using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Rx
{
    [TestClass]
    public class Inspections
    {
        [TestMethod]
        public void Any_true()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            var any = subject
                .Any()
                .Subscribe(b => b.Should().BeTrue());

            subject.OnNext(1);
            subject.OnNext(2);

            subject.OnCompleted();
        }

        [TestMethod]
        public void Any_false()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            var any = subject
                .Any()
                .Subscribe(b => b.Should().BeFalse());

            subject.OnCompleted();
        }

        [TestMethod]
        public void Any_with_filter()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            var any = subject
                .Any(a => a == 2)
                .Subscribe(b => b.Should().BeTrue());

            subject.OnNext(1);
            subject.OnNext(2);

            subject.OnCompleted();
        }

        [TestMethod]
        public void All()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            var all = subject.All(i => i < 5);

            all.Print("All", "All values less than 5? {0}");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(6);
            subject.OnNext(2);
            subject.OnNext(1);

            subject.OnCompleted();
        }

        [TestMethod]
        public void Contains()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            var contains = subject.Contains(2);

            contains.Print("Contains", "Contains the value 2? {0}");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);

            subject.OnCompleted();
        }
    }
}
