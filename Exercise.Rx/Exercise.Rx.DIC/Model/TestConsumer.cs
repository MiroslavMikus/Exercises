using System;
using System.Reactive.Linq;

namespace Exercise.Rx.DIC
{
    public class TestConsumer
    {
        public TestConsumer(IObservableProvider<int> observable)
        {
            observable.Observable.Subscribe(a => Console.WriteLine(a));
        }
    }
}
