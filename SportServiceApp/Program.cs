using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using SportServiceApp.Data;

namespace SportServiceApp
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main function");
                var host = CreateHostBuilder(args).Build();
                CreateDbIfNotExists(host);
                host.Run();

                // CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in init");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.EnsureCreated();
                    context.Database.Migrate();

                    //// requires using Microsoft.Extensions.Configuration;
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    // Set password with the Secret Manager tool.
                    // dotnet user-secrets set SeedUserPW <pw>

                    var testUserPw = config["SeedUserPW"];
                    var testPublicPw = config["SeedPublicUserPW"];
                    SeedRoles.Initialize(services, testUserPw, testPublicPw).Wait();

                    // using ContosoUniversity.Data; 
                    SeedData.SeedDatabase(context);
                  
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.ClearProviders();
                        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                        logging.AddDebug();
                       logging.AddConsole();
                        //EventSource, EventLog, TraceSource, AzureAppServiceFile, AzureAppServicesBlob, ApplicationInsights
                    }).UseNLog();
                    //.ConfigureLogging(logging =>
                    //{
                    //    logging.ClearProviders();
                    //    logging.SetMinimumLevel(LogLevel.Information);
                    //}).UseNLog();
                });
    }
}

