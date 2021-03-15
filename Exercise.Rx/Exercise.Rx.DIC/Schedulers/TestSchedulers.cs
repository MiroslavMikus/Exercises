using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;

namespace Exercise.Rx.DIC
{
    public sealed class TestSchedulers : ISchedulerProvider
    {
        public TestScheduler CurrentThread { get; } = new TestScheduler();
        public TestScheduler Dispatcher { get; } = new TestScheduler();
        public TestScheduler Immediate { get; } = new TestScheduler();
        public TestScheduler NewThread { get; } = new TestScheduler();
        public TestScheduler ThreadPool { get; } = new TestScheduler();
        public TestScheduler TaskPool { get; } = new TestScheduler();

        #region Explicit implementation of ISchedulerService
        IScheduler ISchedulerProvider.CurrentThread { get { return CurrentThread; } }
        IScheduler ISchedulerProvider.Dispatcher { get { return Dispatcher; } }
        IScheduler ISchedulerProvider.Immediate { get { return Immediate; } }
        IScheduler ISchedulerProvider.NewThread { get { return NewThread; } }
        IScheduler ISchedulerProvider.ThreadPool { get { return ThreadPool; } }
        IScheduler ISchedulerProvider.TaskPool { get { return TaskPool; } }
        #endregion
    }
}
