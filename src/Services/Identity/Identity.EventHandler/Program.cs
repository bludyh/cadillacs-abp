﻿using AutoMapper;
using Identity.Common.Data;
using Identity.EventHandler.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pitstop.Infrastructure.Messaging.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Identity.EventHandler
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddJsonFile($"appsettings.json", optional: false);
                    configHost.AddEnvironmentVariables();
                    configHost.AddEnvironmentVariables("DOTNET_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.UseRabbitMQMessageHandler(hostContext.Configuration);

                    services.AddTransient(_ =>
                    {
                        var sqlConnectionString = hostContext.Configuration.GetConnectionString("Identity");
                        var dbContextOptions = new DbContextOptionsBuilder<IdentityContext>()
                            .UseSqlServer(sqlConnectionString)
                            .Options;
                        var dbContext = new IdentityContext(dbContextOptions);

                        return dbContext;
                    });

                    services.AddHostedService<EventHandler>();

                    services.AddAutoMapper(typeof(MappingProfile));
                })
                .UseConsoleLifetime();

            return hostBuilder;
        }

    }
}
