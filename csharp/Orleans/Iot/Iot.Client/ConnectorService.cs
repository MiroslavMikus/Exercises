using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Spectre.Console;

namespace Iot.Client
{
    public class ConnectorService : IHostedService
    {
        private static void WriteLogMessage(string message)
        {
            AnsiConsole.MarkupLine($"[grey]LOG:[/] {message}[grey]...[/]");
        }
        
        private readonly ILogger<ConnectorService> _logger;
        public IClusterClient Client { get; }

        public ConnectorService(ILogger<ConnectorService> logger)
        {
            _logger = logger;
            Client = new ClientBuilder()
                .UseLocalhostClustering()
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await AnsiConsole.Status().StartAsync("Connecting...", async ctx =>
            {
                var retries = 100;
                await Client.Connect(async error =>
                {
                    if (--retries < 0)
                    {
                        WriteLogMessage("[red]Error[/] Connecting: could not connect to cluster");
                        return false;
                    }
                    else
                    {
                        WriteLogMessage($"[yellow]Warning[/] Connecting: could not connect to cluster, retry [purple]{retries}[/]");
                    }

                    try
                    {
                        await Task.Delay(1000, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        return false;
                    }

                    return true;
                });
            });

            WriteLogMessage("[bold green]Connected.[/]");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            var cancellation = new TaskCompletionSource<bool>();
            cancellationToken.Register(() => cancellation.TrySetCanceled(cancellationToken));

            return Task.WhenAny(Client.Close(), cancellation.Task);
        }
    }
}