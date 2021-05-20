using System;
using System.Threading;
using System.Threading.Tasks;
using Iot.GrainClasses.Implementation;
using Iot.GrainClasses.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Spectre.Console;

namespace Iot.Client
{
    public class CommandService : IHostedService
    {
        private readonly IClusterClient _client;
        private readonly IHost _host;
        private readonly ILogger<CommandService> _logger;
        private Task _execution;

        public CommandService(IClusterClient client, IHost host, ILogger<CommandService> logger)
        {
            _client = client;
            _host = host;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _execution = RunAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[green]Select command[/]");
            
                var command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What's your [green]favorite fruit[/]?")
                        .PageSize(10)
                        .AddChoices(new[] {"join", "set", "observe","exit"}));

                try
                {
                    switch (command)
                    {
                        case "join":
                            var deviceId = AnsiConsole.Prompt(new TextPrompt<int>("Enter device ID:"));
                            var systemId = AnsiConsole.Prompt(new TextPrompt<string>("Enter System ID:"));
                            var device = _client.GetGrain<IDeviceGrain>(deviceId);
                            await device.JoinSystem(systemId);

                            break;
                        case "set":
                            var setValue = AnsiConsole.Prompt(new TextPrompt<string>("Enter [red]deviceId,value[/] to be set:"));
                            var decoder = _client.GetGrain<IDecodeGrain>(Guid.Empty);
                            await decoder.Decode(setValue);
                            break;

                        case "observe":
                            var systemIdToObserve = AnsiConsole.Prompt(new TextPrompt<string>("Enter system ID:"));
                            var system = _client.GetGrain<ISystemGrain>(systemIdToObserve);
                            var observer = await _client.CreateObjectReference<ISystemObserver>(new SystemObserver());
                            await system.Subscribe(observer);
                            break;
                        case "exit":
                            await _host.StopAsync();
                            return; 
                        default:
                            Console.WriteLine("Invalid command");
                            break;
                    }

                    Console.WriteLine("Command done");
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "grain failed");
                }
            }
        }
    }
}