using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotorcycleService.Core.Dtos;
using MotorcycleService.Core.Integrations;
using System.Text.Json;

namespace MotorcycleService.Integrations.RentService
{
    public class RentApi : IRentApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RentApi> _logger;

        public RentApi(
            IConfiguration configuration,
            ILogger<RentApi> logger)
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
                var response = await _httpClient.GetAsync($"/locacao/{motoId}/moto");
                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var rent = JsonSerializer.Deserialize<RentDto>(responseData);
                return rent;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
