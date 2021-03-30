using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace HalloConfig
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}