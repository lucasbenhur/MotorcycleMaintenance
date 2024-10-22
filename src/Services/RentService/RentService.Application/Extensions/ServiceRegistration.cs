using Microsoft.Extensions.DependencyInjection;
using RentService.Application.Services;

namespace RentalService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var httpClient = new HttpClient();
            services.AddSingleton(httpClient);

            services.AddSingleton<IMotorcycleService, MotorcycleService>();
            services.AddSingleton<IDeliveryManService, DeliveryManService>();

            return services;
        }
    }
}
