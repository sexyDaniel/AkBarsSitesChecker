using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using SiteCheck.Application;
using SiteCheck.Application.Common.Mapping;
using SiteCheck.Application.Interfaces;
using SiteCheck.OpenTelemetry.Metrics.Constants;
using SiteCheck.Persistence;
using SiteCheck.Web.BackgroundServices;
using SiteCheck.Web.Hubs;
using System.Reflection;

namespace SiteCheck.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(x => x.AddService("Site.Checker"))
                .WithMetrics(x =>
                {
                    x.AddAspNetCoreInstrumentation();
                    x.AddMeter(MeterNames.RegistrationMeter);
                    x.AddOtlpExporter((exporterOptions, readerOpteons) =>
                    {
                        exporterOptions.Endpoint = new System.Uri(Configuration.GetSection("Prometheus:Url").Value);
                        exporterOptions.Protocol = OtlpExportProtocol.HttpProtobuf;
                        readerOpteons.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 3000;
                    });
                });

            services.AddAutoMapper(conf=> 
            {
                conf.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                conf.AddProfile(new AssemblyMappingProfile(typeof(IDbContext).Assembly));

            });
            services.AddPersistence(Configuration);
            services.AddApplication();
            services.AddHttpClient();
            services.AddSignalR();
            services.AddHostedService<CheckSitesBackgroundService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options=> 
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ReloadSitesInfo>("/ReloadInfo");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
