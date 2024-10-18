using Microsoft.OpenApi.Models;

namespace MotorcycleService
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
                    Title = "Serviço de Motos",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        foreach (var path in swaggerDoc.Paths.ToList())
                        {
                            var adjustedPath = $"/motos{path.Key}".TrimEnd('/');

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
