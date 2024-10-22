using RentService.Application.Entities;

namespace RentService.Application.Services
{
    public interface IMotorcycleService
    {
        Task<Motorcycle?> Get(string id);
    }
}
