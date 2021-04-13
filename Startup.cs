using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WINTEX.DAL;
using WINTEX.Data;
using WINTEX.Models.Authentication;

namespace WINTEX
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
            // Adds DB context for ApplicationDb/UserDb and configures identity authentication resources
            // From Here
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuthenicationSqlServer")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            // Up to Here

            services.AddDatabaseDeveloperPageExceptionFilter();
            // Takes advantage of scaffolded identity
            services.AddRazorPages();
            // Globally auto validate UNSAFE method request's antiforgery token
            services.AddControllersWithViews(options => 
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            // Application db server
            services.AddDbContext<FEGBExcavationContext>(options => {
                options.UseNpgsql(Configuration["ConnectionStrings:FagElGamousPostGres"]);
                });
            // Creates the logger and defines the sinks
            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                .MinimumLevel.Override("WINTEX", LogEventLevel.Information)
                                .Enrich.WithUserName("ANONYMOUS","NULL")
                                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} by {User}{NewLine}{Exception}")
                                .WriteTo.Debug()
                                .WriteTo.File(new JsonFormatter(), "Log/log-.log", rollingInterval: RollingInterval.Day)
                                .CreateLogger();
            // Adds UnitOfWork to DI
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Allows controllers to use tempdata
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Attaches the request user to every HTTP request made to the server
            app.UseSerilogRequestLogging(options =>
            {
                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("User", httpContext.User.Identity.Name);
                };
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
