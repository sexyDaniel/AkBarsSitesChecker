using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiteCheck.Application.Interfaces;

namespace SiteCheck.Persistence
{
    public static class DI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(b => b.MigrationsAssembly("SiteCheck.Web"));
            });

            services.AddScoped<IDbContext, AppDbContext>();

            return services;
        }
    }
}
