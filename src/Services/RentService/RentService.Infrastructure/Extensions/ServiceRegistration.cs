using Microsoft.Extensions.DependencyInjection;
using RentService.Core.Repositories;
using RentService.Infrastructure.Data;
using RentService.Infrastructure.Repositories;

namespace RentService.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRentDbContext, RentDbContext>();
            services.AddScoped<IRentRepository, RentRepository>();

            return services;
        }
    }
}
