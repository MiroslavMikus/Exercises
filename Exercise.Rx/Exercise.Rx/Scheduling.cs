using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercise.Rx
{
    [TestClass]
    public class Scheduling
    {
        [TestMethod]
        public void SubscribeOn()
        {
            Console.WriteLine("Starting on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

            var source = Observable.Create<int>(
            o =>
            {
                Console.WriteLine("Invoked on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

                for (int i = 1; i < 15; i++)
                {
                    o.OnNext(i);
                }
                o.OnCompleted();

                Console.WriteLine("Finished on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

                return Disposable.Empty;
            });

            source
                .SubscribeOn(Scheduler.Default)
                .Subscribe(
                    o =>
                    {
                        Console.WriteLine("Received {1} on threadId:{0}", Thread.CurrentThread.ManagedThreadId, o);
                        Thread.Sleep(100);
                        Console.WriteLine(DateTime.Now);
                    },
                    () => Console.WriteLine("OnCompleted on threadId:{0}",
                        Thread.CurrentThread.ManagedThreadId));

            Console.WriteLine("Subscribed on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(500);
        }
    }
}
