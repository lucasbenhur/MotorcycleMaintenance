using Microsoft.Extensions.DependencyInjection;
using MotorcycleService.Core.Integrations;
using MotorcycleService.Integrations.RentService;

namespace MotorcycleService.Integrations.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIntegrationsServices(this IServiceCollection services)
        {
            services.AddScoped<IRentApi, RentApi>();

            return services;
        }
    }
}
