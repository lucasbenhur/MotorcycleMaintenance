using Microsoft.Extensions.Configuration;
using RentService.Core.Dtos;
using RentService.Core.Integrations;
using Shared.AppLog.Services;
using System.Text.Json;

namespace RentService.Integrations.DeliveryManService
{
    public class DeliveryManApi : IDeliveryManApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger _logger;

        public DeliveryManApi(
            IConfiguration configuration,
            IAppLogger logger)
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
#if DEBUG
                var response = await _httpClient.GetAsync($"/entregadores/{id}");
#else
                var response = await _httpClient.GetAsync($"/{id}");
#endif
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var deliveryMan = JsonSerializer.Deserialize<DeliveryManDto>(responseData);
                return deliveryMan;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer uma requisição para DeliveryManApi");
                return null;
            }
        }
    }
}
