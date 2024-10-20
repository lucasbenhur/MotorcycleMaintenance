﻿using DeliveryManService.Application.Handlers;
using DeliveryManService.Core.Repositories;
using DeliveryManService.Infrastructure.Data;
using DeliveryManService.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Shared.ServiceContext;
using System.Reflection;

namespace DeliveryManService.Api
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
                    Title = "Entregadores API",
                    Description = "Sistema de Manutenção de Motos",
                    Version = "v1"
                });
                c.EnableAnnotations();
            });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateDeliveryManCommandHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            builder.Services.AddScoped<IServiceContext, ServiceContext>();
            builder.Services.AddScoped<IDeliveryManDbContext, DeliveryManDbContext>();
            builder.Services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();

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
                            var adjustedPath = $"/entregadores{path.Key}".TrimEnd('/');
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
                    path = "/entregadores";

                c.SwaggerEndpoint($"{path}/swagger/v1/swagger.json", "Entregadores API v1");
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
