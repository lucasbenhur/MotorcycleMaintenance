using DeliveryManService.Core.Repositories;
using DeliveryManService.Infrastructure.Data;
using DeliveryManService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shared.ServiceContext;

namespace DeliveryManService.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceContext, ServiceContext>();
            services.AddScoped<IDeliveryManDbContext, DeliveryManDbContext>();
            services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();

            return services;
        }
    }
}
