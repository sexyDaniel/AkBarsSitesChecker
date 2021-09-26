using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiteCheck.Application.Interfaces;
using SiteCheck.Domain;
using SiteCheck.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SiteCheck.Web.BackgroundServices
{
    public class CheckSitesBackgroundService : IHostedService, IDisposable
    {
        private List<Timer> timers = new List<Timer>();
        private List<Site> unique = new List<Site>();
        private readonly IHttpClientFactory httpClientFactory;
        public IServiceProvider Services { get; }

        public CheckSitesBackgroundService( IHttpClientFactory httpClientFactory, IServiceProvider services) =>(this.httpClientFactory,Services) = (httpClientFactory,services);
        public void Dispose()
        {
            foreach (var t in timers) 
            {
                t?.Dispose();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            List<Site> sites = new List<Site>();
            using (var scope = Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AppDbContext>();
                    sites = context.Sites.Where(s=>!s.IsAddNow).ToList();
                }
                catch (Exception exception)
                {
                }
            }
            timers.Add(new Timer(CheckNewSites, null, 0, 2000));
            foreach (var s in sites) 
            {
                timers.Add(new Timer(HistoryFormation, s, 0,s.SecondCount * 2000));
            }
            return Task.CompletedTask;
        }

       

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var t in timers) 
            {
                t?.Change(Timeout.Infinite, 0);
            }

            return Task.CompletedTask;
        }

        private async void CheckNewSites(object state) 
        {
            using (var scope = Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AppDbContext>();
                    var newSites = context.Sites.Where(s=>s.IsAddNow).ToList();
                    foreach (var s in newSites) 
                    {
                        s.IsAddNow = false;
                        context.Update(s);
                        await context.SaveChangesAsync();
                        timers.Add(new Timer(HistoryFormation, s, 0, s.SecondCount * 1000));
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }

        private async void HistoryFormation(object state)
        {
            var site = state as Site;
            if (site != null)
            {
                var client = httpClientFactory.CreateClient();
                var responce = await client.GetAsync($"https://{site.SiteLink}");
                using (var scope = Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    try
                    {
                        var context = serviceProvider.GetRequiredService<AppDbContext>();
                        context.Histories.Add(new History
                        {
                            SiteId = site.Id,
                            Date = DateTime.Now,
                            IsAvailable = responce.StatusCode == System.Net.HttpStatusCode.OK
                        });
                        site.IsAvailable = responce.StatusCode == System.Net.HttpStatusCode.OK;
                        context.Update(site);
                        await context.SaveChangesAsync();
                        Console.WriteLine($"Check site : {site.SiteLink}");
                    }
                    catch (Exception exception)
                    {
                    }
                }
            }
        }
    }
}
