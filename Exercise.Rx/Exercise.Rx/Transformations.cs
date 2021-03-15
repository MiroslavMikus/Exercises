using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Rx
{

    [TestClass]
    public class Transformations
    {
        [TestMethod]
        public void Select()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            subject.Select(a => a + 3)
                .Print("Select");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);

            subject.OnCompleted();
        }
        [TestMethod]

        public void Select_char()
        {
            var subject = new Subject<int>();

            subject.Print("Subject");

            subject.Select(i => new { Number = i, Character = (char)(i + 64) })
                .Print("Select");

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);

            subject.OnCompleted();
        }
    }
}
