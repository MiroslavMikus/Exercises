using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Filtering;
using App.Metrics.Gauge;
using App.Metrics.Scheduling;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Metrics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var metrics = new MetricsBuilder()
                .OutputMetrics.AsPlainText()
                .OutputMetrics.AsJson()
                .Report.ToConsole()
                .Build();

            TryCpuGauge(metrics);

            await TryTimer(metrics);

            await Task.WhenAll(metrics.ReportRunner.RunAllAsync());

            await ReportEnviromentInfo(metrics);
            await ReportFormatedSnapshot(metrics, metrics.Snapshot.Get());

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void TryCpuGauge(IMetricsRoot metrics)
        {
            var processPhysicalMemoryGauge = new GaugeOptions
            {
                Name = "Process Physical Memory",
                MeasurementUnit = Unit.MegaBytes
            };

            var process = Process.GetCurrentProcess();

            metrics.Measure.Gauge.SetValue(processPhysicalMemoryGauge, process.WorkingSet64 / 1024 / 1024);
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
                async () =>
                {
                    await Task.WhenAll(metrics.ReportRunner.RunAllAsync());
                });

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

        private static async Task ReportSnapshot(IMetricsRoot metrics, MetricsDataValueSource snapshot)
        {
            using (var stream = new MemoryStream())
            {
                await metrics.DefaultOutputMetricsFormatter.WriteAsync(stream, snapshot);

                var result = Encoding.UTF8.GetString(stream.ToArray());

                System.Console.WriteLine(result);
            }
        }

        private static async Task ReportFormatedSnapshot(IMetricsRoot metrics, MetricsDataValueSource snapshot)
        {
            foreach (var formatter in metrics.OutputMetricsFormatters)
            {
                using (var stream = new MemoryStream())
                {
                    await formatter.WriteAsync(stream, snapshot);

                    var result = Encoding.UTF8.GetString(stream.ToArray());

                    System.Console.WriteLine(result);
                }
            }
        }

        private static async Task ReportEnviromentInfo(IMetricsRoot metrics)
        {
            using (var stream = new MemoryStream())
            {
                await metrics.DefaultOutputEnvFormatter.WriteAsync(stream, metrics.EnvironmentInfo);
                var result = Encoding.UTF8.GetString(stream.ToArray());
                System.Console.WriteLine(result);
            }
        }
    }
}
