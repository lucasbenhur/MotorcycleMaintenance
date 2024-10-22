using Microsoft.OpenApi.Models;
using RentService.Application.Commands;
using RentService.Infrastructure.Extensions;
using RentService.Integrations.Extensions;
using Shared.ServiceContext;
using System.Reflection;

namespace RentService.Api
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
                    Title = "Locação API",
                    Description = "Sistema de Manutenção de Motos",
                    Version = "v1"
                });
                c.EnableAnnotations();
            });

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateRentCommand).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            builder.Services.AddScoped<IServiceContext, ServiceContext>();
            builder.Services.AddIntegrationsServices();
            builder.Services.AddInfrastructureServices();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        foreach (var path in swaggerDoc.Paths.ToList())
                        {
                            var adjustedPath = $"/locacao{path.Key}".TrimEnd('/');

                            swaggerDoc.Paths[adjustedPath] = path.Value;
                            swaggerDoc.Paths.Remove(path.Key);
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
                    path = "/locacao";

                c.SwaggerEndpoint($"{path}/swagger/v1/swagger.json", "Locação API v1");
            });

            app.MapControllers();

            if (!app.Environment.IsDevelopment())
                app.Run("http://*:80");

            app.Run();
        }
    }
}
