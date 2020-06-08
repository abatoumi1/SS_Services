using ContosoUniversity.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace ContosoUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {        
           var host = CreateWebHostBuilder(args).Build();
           CreateDbIfNotExists(host);
           host.Run();
        }

        private static void CreateDbIfNotExists(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ContosoUniversityContext>();
                    context.Database.EnsureCreated();

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
                .UseStartup<Startup>();


        //    public static IHostBuilder CreateHostBuilder(string[] args) =>
        //       Host.CreateDefaultBuilder(args)
        //           .ConfigureWebHostDefaults(webBuilder =>
        //           {
        //               webBuilder.UseStartup<Startup>();
        //           });
    }
}
