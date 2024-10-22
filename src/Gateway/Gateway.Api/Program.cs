using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("ocelot.json");
            builder.Services.AddOcelot();

            var app = builder.Build();

            app.MapControllers();
            app.UseOcelot().Wait();

            if (!app.Environment.IsDevelopment())
                app.Run("http://*:80");

            app.Run();
        }
    }
}
