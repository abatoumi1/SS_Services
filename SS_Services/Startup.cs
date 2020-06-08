using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SS_Services.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace SS_Services
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            //Add framework services.
           services.AddDbContext<IdentityDataContext>(options =>
              options.UseSqlServer(Configuration
                  ["Data:Identity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();

            //services.AddDbContext<DataContext>(options =>
            //     options.UseSqlServer(Configuration
            //         ["Data:Products:ConnectionString"]));

            services.AddDbContext<DataContext>(options =>
                 options.UseSqlServer(Configuration
                     ["Data:Products2:ConnectionString"]));

            services.AddMvc().AddJsonOptions(opts => {
                opts.SerializerSettings.ReferenceLoopHandling
                    = ReferenceLoopHandling.Serialize;
                opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString =
                    Configuration["Data:Products:ConnectionString"];
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });

            services.AddSession(options =>
            {
                options.CookieName = "SportsStore.Session";
                options.IdleTimeout = System.TimeSpan.FromHours(48);
                options.CookieHttpOnly = false;
            });

            services.Configure<IdentityOptions>(config => {
                config.Cookies.ApplicationCookie.Events =
                    new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context => {
                            if (context.Request.Path.StartsWithSegments("/api")
                                    && context.Response.StatusCode == 200)
                            {
                                context.Response.StatusCode = 401;
                            }
                            else
                            {
                                context.Response.Redirect(context.RedirectUri);
                            }
                            return Task.FromResult<object>(null);
                        }
                    };
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("AllowAnyOrigin");

            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseMvc();

            //SeedData.SeedDatabase(app.ApplicationServices
            //   .GetRequiredService<DataContext>());
            //IdentitySeedData.SeedDatabase(app);
        }
    }
}
