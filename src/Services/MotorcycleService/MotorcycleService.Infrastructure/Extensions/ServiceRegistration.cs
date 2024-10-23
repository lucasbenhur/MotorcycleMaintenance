using Microsoft.Extensions.DependencyInjection;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Infrastructure.Data;
using MotorcycleService.Infrastructure.Repositories;

namespace MotorcycleService.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleDbContext, MotorcycleDbContext>();
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

            return services;
        }
    }
}
