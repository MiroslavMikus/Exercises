using System;

namespace Exercise.Rx.DIC
{
    public interface IObservableProvider<T>
    {
        IObservable<T> Observable { get; }
    }
}
