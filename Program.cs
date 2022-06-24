﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ROBOT.Services;

namespace ROBOT
{
    internal class MainClass
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var serviceProvider = host.Services;
            try
            {
                var log = host.Services.GetRequiredService<ILogger<MainClass>>();
                log.LogInformation("Host created");
                //await serviceProvider.GetService<IIntegrationService>().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(
                (serviceCollection) => ConfigureServices(serviceCollection));
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(config => config.AddDebug().AddConsole());

            
        }
    }
}