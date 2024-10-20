using MotorcycleService.Core.Entities;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Infrastructure.Data;

namespace MotorcycleService.Infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        public IMotorcycleContext _context { get; }

        public MotorcycleRepository(IMotorcycleContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> CreateMotorcycle(Motorcycle motorcycle)
        {
            await _context.Motorcycles.InsertOneAsync(motorcycle);
            return motorcycle;
        }
    }
}
