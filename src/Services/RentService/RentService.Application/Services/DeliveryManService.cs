using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RentService.Application.Entities;

namespace RentService.Application.Services
{
    public class DeliveryManService : IDeliveryManService
    {
        private readonly HttpClient _httpClient;

        public DeliveryManService(IConfiguration configuration)
        {
            var baseUri = configuration["ServicesSettings:DeliveryMan:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        public async Task<DeliveryMan?> Get(string id)
        {
            var response = await _httpClient.GetAsync($"/{id}");
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            var deliveryMan = JsonSerializer.Deserialize<DeliveryMan>(responseData);
            return deliveryMan;
        }
    }
}
