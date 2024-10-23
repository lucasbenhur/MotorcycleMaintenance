using Microsoft.Extensions.DependencyInjection;
using Shared.AppLog.Data;
using Shared.AppLog.Repositories;
using Shared.AppLog.Services;

namespace Shared.AppLog.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddLogServices(this IServiceCollection services)
        {
            services.AddScoped<IAppLogDbContext, AppLogDbContext>();
            services.AddScoped<IAppLogRepository, AppLogRepository>();
            services.AddScoped<IAppLogger, AppLogger>();
        }
    }
}
