using Microsoft.Extensions.Configuration;
using RentService.Core.Dtos;
using RentService.Core.Integrations;
using Shared.AppLog.Services;
using System.Text.Json;

namespace RentService.Integrations.MotorcycleService
{
    public class MotorcycleApi : IMotorcycleApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger _logger;

        public MotorcycleApi(
            IConfiguration configuration,
            IAppLogger logger)
        {
            var baseUri = configuration["ServicesSettings:Motorcycle:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };

            _logger = logger;
        }

        public async Task<MotorcycleDto?> GetAsync(string id)
        {
            try
            {
#if DEBUG
                var response = await _httpClient.GetAsync($"/motos/{id}");
#else
                var response = await _httpClient.GetAsync($"/{id}");
#endif
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var motorcycle = JsonSerializer.Deserialize<MotorcycleDto>(responseData);
                return motorcycle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer uma requisição para MotorcycleApi");
                return null;
            }
        }
    }
}
