using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloConfig.ConfigDebouncer
{
    /// <summary>
    /// https://gist.github.com/cocowalla/5d181b82b9a986c6761585000901d1b8
    /// </summary>
    public class Debouncer : IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly TimeSpan _waitTime;
        private int _counter;

        public Debouncer(TimeSpan? waitTime = null)
        {
            this._waitTime = waitTime ?? TimeSpan.FromSeconds(3);
        }

        public void Debouce(Action action)
        {
            var current = Interlocked.Increment(ref this._counter);

            Task.Delay(this._waitTime).ContinueWith(task =>
            {
                // Is this the last task that was queued?
                if (current == this._counter && !this._cts.IsCancellationRequested)
                    action();

                task.Dispose();
            }, this._cts.Token);
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}