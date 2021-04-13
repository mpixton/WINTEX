using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
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
            ///Adds DB context for ApplicationDb/UserDb and configures identity authentication resources
            ///From Here
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AuthenicationSqlServer")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();
            ///Up to Here
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDbContext<FEGBExcavationContext>(options => {
                options.UseNpgsql(Configuration["ConnectionStrings:FagElGamousPostGres"]);
                });

            Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                .MinimumLevel.Override("WINTEX", LogEventLevel.Information)
                                .Enrich.WithUserName("ANONYMOUS","NULL")
                                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message} by {User}{NewLine}{Exception}")
                                .WriteTo.Debug()
                                .WriteTo.File(new JsonFormatter(), "Log/log-.log", rollingInterval: RollingInterval.Day)
                                .CreateLogger();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
                // Map Mummies controller
                endpoints.MapControllerRoute(
                    name: "mummyViewAll",
                    pattern: "ViewAllMummies",
                    new { Controller = "Mummies", action = "Index"}
                    );

                endpoints.MapControllerRoute(
                    name: "mummyEdit",
                    pattern: "EditMummy/{id}",
                    new { Controller = "Mummies", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyDetails",
                    pattern: "Mummy/{id}",
                    new { Controller = "Mummies", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyDelete",
                    pattern: "DeleteMummy/{id}",
                    new { Controller = "Mummies", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyCreate",
                    pattern: "AddMummy",
                    new { Controller = "Mummies", action = "Create" }
                    );

                // Map BiologicalSamples controller
                endpoints.MapControllerRoute(
                    name: "bioSampleViewAll",
                    pattern: "ViewAllBiologicalSamples",
                    new { Controller = "BiologicalSamples", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleEdit",
                    pattern: "EditBioSample/{id}",
                    new { Controller = "BiologicalSamples", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleDetails",
                    pattern: "BioSample/{id}",
                    new { Controller = "BiologicalSamples", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleDelete",
                    pattern: "DeleteBioSample/{id}",
                    new { Controller = "BiologicalSamples", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleCreate",
                    pattern: "AddBioSample",
                    new { Controller = "BiologicalSamples", action = "Create" }
                    );

                // Map BioSamplesNotes controller
                endpoints.MapControllerRoute(
                    name: "bioSampleNotesViewAll",
                    pattern: "ViewAllBiologicalSamplesNotes",
                    new { Controller = "BiologicalSamplesNotes", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleNotesEdit",
                    pattern: "EditBiologicalSamplesNotes/{id}",
                    new { Controller = "BiologicalSamplesNotes", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleNotesDetails",
                    pattern: "BiologicalSamplesNotes/{id}",
                    new { Controller = "BiologicalSamplesNotes", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleNotesDelete",
                    pattern: "DeleteBiologicalSamplesNotes/{id}",
                    new { Controller = "BiologicalSamplesNotes", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "bioSampleNotesCreate",
                    pattern: "AddBiologicalSamplesNotes",
                    new { Controller = "BiologicalSamplesNotes", action = "Create" }
                    );

                // Map CarbonDatings controller
                endpoints.MapControllerRoute(
                    name: "carbonDatingViewAll",
                    pattern: "ViewAllCarbonDatings",
                    new { Controller = "CarbonDatings", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "carbonDatingEdit",
                    pattern: "EditCarbonDating/{id}",
                    new { Controller = "CarbonDatings", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "carbonDatingDetails",
                    pattern: "CarbonDating/{id}",
                    new { Controller = "CarbonDatings", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "carbonDatingDelete",
                    pattern: "DeleteCarbonDating/{id}",
                    new { Controller = "CarbonDatings", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "carbonDatingCreate",
                    pattern: "AddCarbonDating",
                    new { Controller = "CarbonDatings", action = "Create" }
                    );

                // Map Fegbdatums controller
                endpoints.MapControllerRoute(
                    name: "fegbDatumsViewAll",
                    pattern: "ViewAllFEGBdatums",
                    new { Controller = "Fegbdatums", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbDatumsEdit",
                    pattern: "EditFEGBDatum/{id}",
                    new { Controller = "Fegbdatums", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbDatumsDetails",
                    pattern: "FEGBDatum/{id}",
                    new { Controller = "Fegbdatums", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbDatumsDelete",
                    pattern: "DeleteFEGBDatum/{id}",
                    new { Controller = "Fegbdatums", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbDatumsCreate",
                    pattern: "AddFEGBDatum",
                    new { Controller = "Fegbdatums", action = "Create" }
                    );

                // Map FegbmummyStorages controller
                endpoints.MapControllerRoute(
                    name: "fegbMummyStorageViewAll",
                    pattern: "ViewAllFEGBMummyStorages",
                    new { Controller = "FEGBMummyStorages", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbMummyStorageEdit",
                    pattern: "EditFEGBMummyStorage/{id}",
                    new { Controller = "FEGBMummyStorages", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbMummyStorageDetails",
                    pattern: "FEGBMummyStorage/{id}",
                    new { Controller = "FEGBMummyStorages", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbMummyStorageDelete",
                    pattern: "DeleteFEGBMummyStorage/{id}",
                    new { Controller = "FEGBMummyStorages", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "fegbMummyStorageCreate",
                    pattern: "AddFEGBMummyStorage",
                    new { Controller = "FEGBMummyStorages", action = "Create" }
                    );

                // Map Gisdatums controller
                endpoints.MapControllerRoute(
                    name: "GISDatumViewAll",
                    pattern: "ViewAllGisdatums",
                    new { Controller = "Gisdatums", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "GISDatumEdit",
                    pattern: "EditGISDatum/{id}",
                    new { Controller = "Gisdatums", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "GISDatumDetails",
                    pattern: "GISDatum/{id}",
                    new { Controller = "Gisdatums", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "GISDatumDelete",
                    pattern: "DeleteGISDatum/{id}",
                    new { Controller = "Gisdatums", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "GISDatumCreate",
                    pattern: "AddGISDatum",
                    new { Controller = "Gisdatums", action = "Create" }
                    );

                // Map MummyNotes controller
                endpoints.MapControllerRoute(
                    name: "mummyNotesViewAll",
                    pattern: "ViewAllMummyNotes",
                    new { Controller = "MummyNotes", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyNotesEdit",
                    pattern: "EditMummyNote/{id}",
                    new { Controller = "MummyNotes", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyNotesDetails",
                    pattern: "MummyNote/{id}",
                    new { Controller = "MummyNotes", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyNotesDelete",
                    pattern: "DeleteMummyNote/{id}",
                    new { Controller = "MummyNotes", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "mummyNotesCreate",
                    pattern: "AddMummyNote",
                    new { Controller = "MummyNotes", action = "Create" }
                    );

                // Map OsteologicalDatums controller
                endpoints.MapControllerRoute(
                    name: "osteologicalDataViewAll",
                    pattern: "ViewAllOsteologicalMummyDatums",
                    new { Controller = "OsteologicalMummyDatums", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "osteologicalDataEdit",
                    pattern: "EditOsteologicalData/{id}",
                    new { Controller = "OsteologicalMummyDatums", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "osteologicalDataDetails",
                    pattern: "OsteologicalData/{id}",
                    new { Controller = "OsteologicalMummyDatums", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "osteologicalDataDelete",
                    pattern: "DeleteOsteologicalData/{id}",
                    new { Controller = "OsteologicalMummyDatums", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "osteologicalDataCreate",
                    pattern: "AddOsteologicalData",
                    new { Controller = "OsteologicalMummyDatums", action = "Create" }
                    );

                // Map PostExhumationDatums controller
                endpoints.MapControllerRoute(
                    name: "postExhumationDataViewAll",
                    pattern: "ViewAllPostExhumationDatums",
                    new { Controller = "PostExhumationDatums", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "postExhumationDataEdit",
                    pattern: "EditPostExhumationData/{id}",
                    new { Controller = "PostExhumationDatums", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "postExhumationDataDetails",
                    pattern: "PostExhumationData/{id}",
                    new { Controller = "PostExhumationDatums", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "postExhumationDataDelete",
                    pattern: "DeletePostExhumationData/{id}",
                    new { Controller = "PostExhumationDatums", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "postExhumationDataCreate",
                    pattern: "AddPostExhumationData",
                    new { Controller = "PostExhumationDatums", action = "Create" }
                    );

                // Map ShaftLocations controller
                endpoints.MapControllerRoute(
                    name: "shaftLocationsViewAll",
                    pattern: "ViewAllShaftLocations",
                    new { Controller = "ShaftLocations", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "shaftLocationsEdit",
                    pattern: "EditShaftLocation/{id}",
                    new { Controller = "ShaftLocations", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "shaftLocationsDetails",
                    pattern: "ShaftLocation/{id}",
                    new { Controller = "ShaftLocations", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "shaftLocationsDelete",
                    pattern: "DeleteShaftLocation/{id}",
                    new { Controller = "ShaftLocations", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "shaftLocationsCreate",
                    pattern: "AddShaftLocation",
                    new { Controller = "ShaftLocations", action = "Create" }
                    );

                // Map TombLocations controller
                endpoints.MapControllerRoute(
                    name: "tombLocationsViewAll",
                    pattern: "ViewAllTombLocations",
                    new { Controller = "TombLocations", action = "Index" }
                    );

                endpoints.MapControllerRoute(
                    name: "tombLocationsEdit",
                    pattern: "EditTombLocation/{id}",
                    new { Controller = "TombLocations", action = "Edit" }
                    );

                endpoints.MapControllerRoute(
                    name: "tombLocationsDetails",
                    pattern: "TombLocation/{id}",
                    new { Controller = "TombLocations", action = "Details" }
                    );

                endpoints.MapControllerRoute(
                    name: "tombLocationsDelete",
                    pattern: "DeleteTombLocation/{id}",
                    new { Controller = "TombLocations", action = "Delete" }
                    );

                endpoints.MapControllerRoute(
                    name: "tombLocationsCreate",
                    pattern: "AddTombLocation",
                    new { Controller = "TombLocations", action = "Create" }
                    );

                // Map other controllers
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
