using System.Reactive.Concurrency;

namespace Exercise.Rx.DIC
{
    public sealed class SchedulerProvider : ISchedulerProvider
    {
        public IScheduler CurrentThread => Scheduler.CurrentThread;
        public IScheduler Dispatcher => DispatcherScheduler.Current;
        public IScheduler Immediate => Scheduler.Immediate;
        public IScheduler NewThread => NewThreadScheduler.Default;
        public IScheduler ThreadPool => Scheduler.Default;
        public IScheduler TaskPool => TaskPoolScheduler.Default;
    }
}
