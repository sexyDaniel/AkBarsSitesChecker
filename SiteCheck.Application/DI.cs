using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace SiteCheck.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
