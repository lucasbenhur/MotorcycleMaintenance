using Microsoft.Extensions.Configuration;
using MotorcycleService.Core.Dtos;
using MotorcycleService.Core.Integrations;
using Shared.AppLog.Services;
using System.Text.Json;

namespace MotorcycleService.Integrations.RentService
{
    public class RentApi : IRentApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger _logger;

        public RentApi(
            IConfiguration configuration,
            IAppLogger logger)
        {
            var baseUri = configuration["ServicesSettings:Rent:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };

            _logger = logger;
        }

        public async Task<RentDto?> GetByMotorcycleIdAsync(string motoId)
        {
            try
            {
#if DEBUG
                var response = await _httpClient.GetAsync($"/locacao/{motoId}/moto");
#else
                var response = await _httpClient.GetAsync($"/{motoId}/moto");
#endif
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var rent = JsonSerializer.Deserialize<RentDto>(responseData);
                return rent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer uma requisição para RentApi");
                return null;
            }
        }
    }
}
