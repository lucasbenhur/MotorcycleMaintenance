using Microsoft.Extensions.Configuration;
using MotorcycleService.Core.Dtos;
using MotorcycleService.Core.Integrations;
using Shared.AppLog.Services;
using Shared.ServiceContext;
using System.Text.Json;

namespace MotorcycleService.Integrations.RentService
{
    public class RentApi : IRentApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public RentApi(
            IConfiguration configuration,
            IAppLogger logger,
            IServiceContext serviceContext)
        {
            var baseUri = configuration["ServicesSettings:Rent:BaseUri"] ?? "";
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };

            _logger = logger;
            _serviceContext = serviceContext;
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
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var rent = JsonSerializer.Deserialize<RentDto>(responseData);
                return rent;
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível se comunicar com a API de locação";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }
    }
}
