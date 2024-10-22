using Microsoft.Extensions.DependencyInjection;
using RentalService.Core.Repositories;
using RentalService.Infrastructure.Data;
using RentalService.Infrastructure.Repositories;
using RentService.Infrastructure.Data;

namespace RentalService.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IRentalDbContext, RentalDbContext>();
            services.AddSingleton<IRentalRepository, RentalRepository>();

            return services;
        }
    }
}
