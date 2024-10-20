using MotorcycleService.Core.Entities;

namespace MotorcycleService.Core.Repositories
{
    public interface IMotorcycleRepository
    {
        //Task<IEnumerable<Motorcycle>> GetMotorcycles(MotorcycleSpecParams motorcycleSpecParams);
        //Task<Motorcycle> GetMotorcycle(string id);
        Task<Motorcycle> CreateMotorcycle(Motorcycle motorcycle);
        //Task<bool> UpdateMotorcycle(Motorcycle motorcycle);
        //Task<bool> DeleteMotorcycle(string id);
    }
}
