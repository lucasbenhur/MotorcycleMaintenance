using EventBus.Messages.Common;
using MassTransit;
using Microsoft.OpenApi.Models;
using MotorcycleService.Api.EventBusConsumer;
using MotorcycleService.Application.Handlers;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Infrastructure.Data;
using MotorcycleService.Infrastructure.Repositories;
using Shared.Notifications.Extensions;
using Shared.ServiceContext;
using System.Reflection;

namespace MotorcycleService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Motos API",
                    Description = "Sistema de Manutenção de Motos",
                    Version = "v1"
                });
                c.EnableAnnotations();
            });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateMotorcycleCommandHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            builder.Services.AddNotification();
            builder.Services.AddScoped<IServiceContext, ServiceContext>();
            builder.Services.AddScoped<IMotorcycleDbContext, MotorcycleDbContext>();
            builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            builder.Services.AddScoped<CreateMotorcycleConsumer>();

            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<CreateMotorcycleConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.CreateMotorcycleQueue, c =>
                    {
                        c.ConfigureConsumer<CreateMotorcycleConsumer>(ctx);
                    });
                });
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        var adjustedPaths = new Dictionary<string, OpenApiPathItem>();
                        foreach (var path in swaggerDoc.Paths)
                        {
                            var adjustedPath = $"/motos{path.Key}".TrimEnd('/');
                            adjustedPaths[adjustedPath] = path.Value;
                        }

                        swaggerDoc.Paths.Clear();
                        foreach (var path in adjustedPaths)
                        {
                            swaggerDoc.Paths.Add(path.Key, path.Value);
                        }
                    });
                });
            }
            else
            {
                app.UseSwagger();
            }

            app.UseSwaggerUI(c =>
            {
                var path = string.Empty;

                if (!app.Environment.IsDevelopment())
                    path = "/motos";

                c.SwaggerEndpoint($"{path}/swagger/v1/swagger.json", "Motos API v1");
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            if (!app.Environment.IsDevelopment())
                app.Run("http://*:80");

            app.Run();
        }
    }
}
