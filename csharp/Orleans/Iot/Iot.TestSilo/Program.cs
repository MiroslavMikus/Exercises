using System;
using System.Threading.Tasks;
using Iot.GrainClasses.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

namespace Iot.TestSilo
{
    class Program
    {
        static Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            
            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering()
                        .ConfigureApplicationParts(_ => _.AddApplicationPart(typeof(DeviceGrain).Assembly).WithReferences())
                        .AddMemoryGrainStorage("sql")
                        // .AddAdoNetGrainStorage("sql", options =>
                        // {
                        //     options.Invariant = "System.Data.SqlClient";
                        //     options.ConnectionString = configuration.GetConnectionString("localDb");
                        //     options.UseJsonFormat = true;
                        // })
                        .UseDashboard();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}