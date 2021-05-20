using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Iot.Client
{
    class Program
    {
        static Task Main(string[] args)
        {
            return new HostBuilder()
                .ConfigureServices(services => services
                    .AddSingleton<ConnectorService>()
                    .AddSingleton<IHostedService>(_ => _.GetService<ConnectorService>())
                    .AddSingleton(_ => _.GetService<ConnectorService>()?.Client)
                    .AddHostedService<CommandService>()
                    .Configure<ConsoleLifetimeOptions>(_ =>
                    {
                        _.SuppressStatusMessages = true;
                    }))
                .ConfigureLogging(builder => builder
                    .AddConsole())
                .RunConsoleAsync();
        }
    }
}