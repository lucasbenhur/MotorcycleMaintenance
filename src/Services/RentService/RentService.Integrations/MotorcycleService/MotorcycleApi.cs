using Microsoft.Extensions.Configuration;
using RentService.Core.Dtos;
using RentService.Core.Integrations;
using System.Text.Json;

namespace RentService.Integrations.MotorcycleService
{
    public class MotorcycleApi : IMotorcycleApi
    {
        private readonly HttpClient _httpClient;

        public MotorcycleApi(IConfiguration configuration)
        {
            var baseUri = configuration["ServicesSettings:Motorcycle:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        public async Task<MotorcycleDto?> Get(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/motos/{id}");
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var motorcycle = JsonSerializer.Deserialize<MotorcycleDto>(responseData);
                return motorcycle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
