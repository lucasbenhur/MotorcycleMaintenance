using MotorcycleService.Core.Dtos;

namespace MotorcycleService.Core.Integrations
{
    public interface IRentApi
    {
        Task<RentDto?> GetByMotorcycleIdAsync(string motorcycleId);
    }
}
