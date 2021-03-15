using System;
using System.Reactive.Linq;
using System.Threading;

namespace Exercise.Rx
{
    public static class Tools
    {
        public static IDisposable Print<T>(this IObservable<T> observable, string header, string pattern)
        {
            return observable.Subscribe(
                onNext: a => Console.WriteLine($"{header}: " + string.Format(pattern, a)),
                onError: ex => Console.WriteLine($"{header}"),
                onCompleted: () => Console.WriteLine($"{header} completed")
            );
        }

        public static IDisposable Print<T>(this IObservable<T> observable, string header)
        {
            return observable.Subscribe(
                onNext: a => Console.WriteLine($"{header}: " + a.ToString()),
                onError: ex => Console.WriteLine($"{header}"),
                onCompleted: () => Console.WriteLine($"{header} completed")
            );
        }

        public static void PrintThread()
        {
            Console.WriteLine("ThreadID:" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
