using Microsoft.Extensions.DependencyInjection;
using RentService.Core.Integrations;
using RentService.Integrations.DeliveryManService;
using RentService.Integrations.MotorcycleService;

namespace RentService.Integrations.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIntegrationsServices(this IServiceCollection services)
        {
            services.AddScoped<IMotorcycleApi, MotorcycleApi>();
            services.AddScoped<IDeliveryManApi, DeliveryManApi>();

            return services;
        }
    }
}
