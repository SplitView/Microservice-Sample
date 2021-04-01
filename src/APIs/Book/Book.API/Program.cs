
using Book.Application.Common.Interface;

using Common.Infrastructure.Logging.ELK;

using Elastic.Apm.NetCoreAll;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using System;
using System.Linq;
using System.Reflection;

namespace Book.API
{
    public class Program
    {
        private static string environment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public static void Main(string[] args)
        {
            //configure logging first
            ConfigureLogging();

            //then create the host, so that if the host fails we can log errors
            CreateHost(args);
        }

        private static void CreateHost(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<IBookDbContext>();

                        var isMigrationAvailable = context.Database.GetPendingMigrations().ToList();
                        if (isMigrationAvailable.Any())
                        {
                            context.Database.Migrate();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog()
                .UseAllElasticApm();

        private static void ConfigureLogging()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true)
                .Build();
            var elasticUri = configuration["ElasticConfiguration:Uri"];
            ELKLog.ConfigureLogging(configuration, environment, elasticUri, Assembly.GetExecutingAssembly());
        }
    }
}
