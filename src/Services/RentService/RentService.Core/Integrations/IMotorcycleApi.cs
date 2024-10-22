using RentService.Core.Dtos;

namespace RentService.Core.Integrations
{
    public interface IMotorcycleApi
    {
        Task<MotorcycleDto?> GetAsync(string id);
    }
}
