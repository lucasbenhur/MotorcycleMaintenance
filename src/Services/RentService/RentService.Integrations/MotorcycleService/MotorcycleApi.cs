using Microsoft.Extensions.Configuration;
using RentService.Core.Dtos;
using RentService.Core.Integrations;
using Shared.AppLog.Services;
using Shared.ServiceContext;
using System.Text.Json;

namespace RentService.Integrations.MotorcycleService
{
    public class MotorcycleApi : IMotorcycleApi
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public MotorcycleApi(
            IConfiguration configuration,
            IAppLogger logger,
            IServiceContext serviceContext)
        {
            var baseUri = configuration["ServicesSettings:Motorcycle:BaseUri"] ?? string.Empty;
            _httpClient = new()
            {
                BaseAddress = new Uri(baseUri)
            };

            _logger = logger;
            _serviceContext = serviceContext;
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
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                response.EnsureSuccessStatusCode();
                string responseData = await response.Content.ReadAsStringAsync();
                var motorcycle = JsonSerializer.Deserialize<MotorcycleDto>(responseData);
                return motorcycle;
            }
            catch (Exception ex)
            {
                var msg = "Não foi possível se comunicar com a API de motos";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }
    }
}
