using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RentService.Core.Dtos;
using RentService.Core.Integrations;
using System.Text.Json;

namespace RentService.Integrations.DeliveryManService
{
    public class DeliveryManApi : IDeliveryManApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DeliveryManApi> _logger;

        public DeliveryManApi(
            IConfiguration configuration,
            ILogger<DeliveryManApi> logger)
        {
            var baseUri = configuration["ServicesSettings:DeliveryMan:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };

            _logger = logger;
        }

        public async Task<DeliveryManDto?> GetAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{id}");
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var deliveryMan = JsonSerializer.Deserialize<DeliveryManDto>(responseData);
                return deliveryMan;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
