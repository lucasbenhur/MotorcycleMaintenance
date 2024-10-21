using MotorcycleService.Core.Entities;
using MotorcycleService.Core.Specs;

namespace MotorcycleService.Core.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> CreateAsync(Motorcycle motorcycle);
        Task<Motorcycle> GetByPlateAsync(string plate);
        Task<Motorcycle> GetAsync(string id);
        Task<ICollection<Motorcycle>> GetAllAsync(GetAllMotorcyclesSpecParams specParams);
        Task<bool> UpdateAsync(Motorcycle motorcycle);
        Task<bool> DeleteAsync(string id);
    }
}
