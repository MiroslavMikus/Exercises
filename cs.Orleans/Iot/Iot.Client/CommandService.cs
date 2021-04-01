using System;
using System.Threading;
using System.Threading.Tasks;
using Iot.GrainClasses.Implementation;
using Iot.GrainClasses.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;

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

        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("write command" + Environment.NewLine);

                var command = Console.ReadLine();

                if (command == "exit")
                {
                    await _host.StopAsync();
                }

                try
                {
                    switch (command)
                    {
                        case var val when command.StartsWith("/join "):
                            var joinInput = command.Replace("/join ", "").Split(',');

                            var device = _client.GetGrain<IDeviceGrain>(int.Parse(joinInput[0]));

                            await device.JoinSystem(joinInput[1]);

                            break;
                        case var val when command.StartsWith("/set "):
                            var setInput = command.Replace("/set ", "");
                            var decoder = _client.GetGrain<IDecodeGrain>(Guid.Empty);
                            await decoder.Decode(setInput);
                            break;

                        case var val when command.StartsWith("/observe "):
                            var observeInput = command.Replace("/observe ", "");
                            var system = _client.GetGrain<ISystemGrain>(observeInput);
                            var observer = await _client.CreateObjectReference<ISystemObserver>(new SystemObserver());
                            await system.Subscribe(observer);
                            break;
                        case var var when command.StartsWith("/exit"):
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