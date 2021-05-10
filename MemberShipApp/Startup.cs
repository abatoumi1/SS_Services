using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemberShipApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MemberShipApp.Services;
using MemberShipApp.Repositories;
using AutoMapper;
using MemberShipApp.Extensions;
using Serilog;

namespace MemberShipApp
{
    public class Startup
    {
        private DBUpdate db;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<MemberShipContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("MemberShipDBPro")));
                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<MemberShipContext>();
            }
            else
            {
                services.AddDbContext<MemberShipContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("MemberShipDB")));
                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<MemberShipContext>();
            }
            //migration
            services.BuildServiceProvider().GetService<MemberShipContext>().Database.Migrate();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICountryServices, CountryServices>();
            services.AddTransient<IStateServices, StateServices>();
            services.AddTransient<IRegionServices, RegionServices>();
            services.AddTransient<IPositionServices, PositionServices>();
            services.AddTransient<IMemberServices, MemberServices>();
            services.AddTransient<IContributionServices, ContributionServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            db = new DBUpdate(Configuration);
            db.RunDBUpdate();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
