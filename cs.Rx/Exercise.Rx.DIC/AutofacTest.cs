using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using System.Collections.Generic;

namespace Exercise.Rx.DIC
{
    [TestClass]
    public class AutofacTest
    {
        [TestMethod]
        public void Autofac_test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<IntObservable>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<TestSchedulers>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<TestConsumer>();
            var scope = builder.Build();

            var obs = scope.Resolve<IntObservable>();

            var expectedValues = new int[] { 0, 1, 2, 3, 4 };
            var actualValues = new List<int>();

            obs.Observable.Take(5).Subscribe(actualValues.Add);
            var scheduler = scope.Resolve<TestSchedulers>();

            scheduler.Immediate.Start();

            CollectionAssert.AreEqual(expectedValues, actualValues);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var scheduler = new TestSchedulers();
            var expectedValues = new int[] { 0, 1, 2, 3, 4 };
            var actualValues = new List<int>();

            var obs = new IntObservable(scheduler);
            obs.Observable.Take(5).Subscribe(actualValues.Add);

            scheduler.Immediate.Start();

            CollectionAssert.AreEqual(expectedValues, actualValues);
        }
    }
}
