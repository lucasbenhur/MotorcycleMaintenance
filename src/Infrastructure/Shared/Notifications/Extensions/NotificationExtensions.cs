﻿using Microsoft.Extensions.DependencyInjection;
using Shared.Notifications.Data;
using Shared.Notifications.Handlers;
using Shared.Notifications.Repositories;
using System.Reflection;

namespace Shared.Notifications.Extensions
{
    public static class NotificationExtensions
    {
        public static void AddNotification(this IServiceCollection services)
        {
            services.AddScoped<INotificationDbContext, NotificationDbContext>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateNotificationCommandHandler).Assembly
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        }
    }
}
