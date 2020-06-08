using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SportsServices.Models;

namespace SportsServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("Products2")));
            //services.AddDbContext<DataContext>(options =>
            //     options.UseSqlServer(Configuration
            //         ["Data:Products2:ConnectionString"]));

            services.AddDbContext<IdentityDataContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("Identity")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();
            services.AddMvc().AddJsonOptions(opts => {
                opts.SerializerSettings.ReferenceLoopHandling
                    = ReferenceLoopHandling.Serialize;
                opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString =
                    Configuration.GetConnectionString("Products");
                options.SchemaName = "dbo";
                options.TableName = "SessionData";
            });


            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = System.TimeSpan.FromHours(48);
                options.Cookie.HttpOnly = false;
                // Make the session cookie essential
                options.Cookie.IsEssential = false;
            });

            //services.AddSession(options =>
            //{
            //    options..CookieName = "SportsStore.Session";
            //    options.IdleTimeout = System.TimeSpan.FromHours(48);
            //    options.CookieHttpOnly = false;
            //});

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "SportsStore.Session";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            //services.Configure<IdentityOptions>(config => {
            //    config..ApplicationCookie.Events =
            //        new CookieAuthenticationEvents
            //        {
            //            OnRedirectToLogin = context => {
            //                if (context.Request.Path.StartsWithSegments("/api")
            //                        && context.Response.StatusCode == 200)
            //                {
            //                    context.Response.StatusCode = 401;
            //                }
            //                else
            //                {
            //                    context.Response.Redirect(context.RedirectUri);
            //                }
            //                return Task.FromResult<object>(null);
            //            }
            //        };
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

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
