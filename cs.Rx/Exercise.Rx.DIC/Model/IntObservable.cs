using System;
using System.Linq;
using System.Reactive.Linq;

namespace Exercise.Rx.DIC
{
    public class IntObservable : IObservableProvider<int>
    {
        public IObservable<int> Observable { get; }
        public IntObservable(ISchedulerProvider scheduler)
        {
            Observable = System.Reactive.Linq.Observable.Interval(TimeSpan.FromSeconds(1), scheduler.TaskPool)
                .Select(a => (int)a);
        }
    }
}
