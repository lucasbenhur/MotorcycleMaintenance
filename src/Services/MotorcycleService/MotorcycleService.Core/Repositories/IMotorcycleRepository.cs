using MotorcycleService.Core.Entities;
using MotorcycleService.Core.Specs;

namespace MotorcycleService.Core.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> CreateAsync(Motorcycle motorcycle);
        Task<Motorcycle> GetByPlateAsync(string plate);
        Task<Motorcycle> GetAsync(string id);
        Task<ICollection<Motorcycle>> GetAllAsync(GetAllMotorcycleSpecParams specParams);
        //Task<bool> UpdateMotorcycle(Motorcycle motorcycle);
        //Task<bool> DeleteMotorcycle(string id);
    }
}
