using System;

using MemberShipApp.Data;
using MemberShipApp.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MemberShipApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try {
                Log.Information("Application Starting Up.");
                var host = CreateWebHostBuilder(args).Build();
                WebHost.CreateDefaultBuilder(args).UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                CreateDbIfNotExists(host);
                host.Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex,"Application failed to start correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        private static void CreateDbIfNotExists(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MemberShipContext>();
                   // context.Database.EnsureCreated();

                    // using ContosoUniversity.Data; 
                    DbInitializer.Initialize(context);

                   
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartup<Startup>();
    }
}
