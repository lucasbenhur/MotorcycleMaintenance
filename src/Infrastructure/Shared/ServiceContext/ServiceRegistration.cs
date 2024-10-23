using Microsoft.Extensions.DependencyInjection;

namespace Shared.ServiceContext
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceContext(this IServiceCollection services)
        {
            services.AddScoped<IServiceContext, ServiceContext>();

            return services;
        }
    }
}
