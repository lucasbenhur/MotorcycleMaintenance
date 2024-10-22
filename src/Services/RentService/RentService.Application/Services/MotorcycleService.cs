using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RentService.Application.Entities;

namespace RentService.Application.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly HttpClient _httpClient;

        public MotorcycleService(IConfiguration configuration)
        {
            var baseUri = configuration["ServicesSettings:Motorcycle:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        public async Task<Motorcycle?> Get(string id)
        {
            var response = await _httpClient.GetAsync("/{id}");
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            var motorcycle = JsonSerializer.Deserialize<Motorcycle>(responseData);
            return motorcycle;
        }
    }
}
