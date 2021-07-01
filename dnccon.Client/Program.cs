using dnccon.Client.Services;
using dnccon.Client.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace dnccon.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


            Log.Logger.Information("Application Starting");

            // Hosting Application in Console.

            var host = Host.CreateDefaultBuilder()

                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IGreetingService, GreetingService>();
                })
                .UseSerilog()
                .Build();

            var services = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);

            services.Run();
                
        }

        static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
              .AddEnvironmentVariables();
        }


    }
}
