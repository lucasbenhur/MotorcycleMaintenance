using Microsoft.OpenApi.Models;

namespace DeliveryMenService
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
                    Title = "Serviço de Entregadores",
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
                            var adjustedPath = $"/entregadores{path.Key}".TrimEnd('/');

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
