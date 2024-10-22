using RentService.Core.Entities;

namespace RentService.Core.Repositories
{
    public interface IRentRepository
    {
        Task<Rent> CreateAsync(Rent rent);
        Task<Rent?> GetAsync(string id);
        Task<Rent?> GetMotorcycleRentAsync(string motorcycleId);
        Task<bool> UpdateAsync(Rent rent);
    }
}
