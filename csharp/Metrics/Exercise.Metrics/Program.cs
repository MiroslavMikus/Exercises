using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Filtering;
using App.Metrics.Gauge;
using App.Metrics.Scheduling;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise.Metrics.Menu;

namespace Exercise.Metrics
{
    static class Program
    {
        private static IMetricsRoot Metrics;

        static async Task Main(string[] args)
        {
            Metrics = new MetricsBuilder()
                .OutputMetrics.AsJson()
                .OutputMetrics.AsPlainText()
                .Report.ToConsole(a => a.FlushInterval = TimeSpan.Zero)
                .Build();

            // TryConnectionGaugeCommand();
            // TryCpuGaugeCommand();
            var menu = new SpectreMenuPrinter();
            await menu.Print();
        }

        public static async Task TryConnectionGaugeCommand()
        {
            var counters = Enumerable.Range(1, 10)
                .Select(a => MetricsRegistry.Connection(a));

            foreach (var c in counters)
            {
                Metrics.Measure.Gauge.SetValue(c, 1);
            }

            // await ReportFormatedSnapshotAsyncCommand();
        }

        public static void TryCpuGaugeCommand()
        {
            var processPhysicalMemoryGauge = new GaugeOptions
            {
                Name = "Process Physical Memory",
                MeasurementUnit = Unit.MegaBytes
            };

            var process = Process.GetCurrentProcess();

            Metrics.Measure.Gauge.SetValue(processPhysicalMemoryGauge, process.WorkingSet64 / 1024 / 1024);
        }

        private static async Task TryTimer(IMetricsRoot metrics)
        {
            using (metrics.Measure.Timer.Time(MetricsRegistry.SampleTimer))
            {
                await TryCounter(metrics, 6);
            }

            using (metrics.Measure.Timer.Time(MetricsRegistry.SampleTimer))
            {
                await TryCounter(metrics, 3);
            }
        }

        private static IDisposable RunTimer(IMetricsRoot metrics)
        {
            var scheduler = new AppMetricsTaskScheduler(
                TimeSpan.FromSeconds(2),
                async () => { await Task.WhenAll(metrics.ReportRunner.RunAllAsync()); });

            scheduler.Start();

            return scheduler;
        }

        private static async Task TryCounter(IMetricsRoot metrics, int count)
        {
            var tags = new MetricTags("server", metrics.EnvironmentInfo.MachineName);

            for (int i = 0; i < count; i++)
            {
                await Task.Delay(500);
                Console.WriteLine($"Incrementing SampleCounter - {i}");
                metrics.Measure.Counter.Increment(MetricsRegistry.SampleCounter, tags);
            }
        }

        public static async Task ReportSnapshotCommand()
        {
            await using var stream = new MemoryStream();

            await Metrics.DefaultOutputMetricsFormatter.WriteAsync(stream, Metrics.Snapshot.Get());

            var result = Encoding.UTF8.GetString(stream.ToArray());

            await Console.Out.WriteLineAsync(result);
        }

        public static async Task ReportEnviromentInfoCommand()
        {
            await using var stream = new MemoryStream();

            await Metrics.DefaultOutputEnvFormatter.WriteAsync(stream, Metrics.EnvironmentInfo);

            var result = Encoding.UTF8.GetString(stream.ToArray());

            await Console.Out.WriteLineAsync(result);
        }
    }
}