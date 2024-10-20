using MongoDB.Driver;
using MotorcycleService.Core.Entities;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Core.Specs;
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

        public async Task<Motorcycle> CreateAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.InsertOneAsync(motorcycle);
            return motorcycle;
        }

        public async Task<Motorcycle> GetByPlateAsync(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
                return null;

            return await _context
                .Motorcycles
                .Find(m => m.Plate.ToUpper() == plate.ToUpper())
                .FirstOrDefaultAsync();
        }

        public async Task<Motorcycle> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return await _context
                .Motorcycles
                .Find(m => m.Id.ToUpper() == id.ToUpper())
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Motorcycle>> GetAllAsync(MotorcycleSpecParams motorcycleSpecParams)
        {
            var builder = Builders<Motorcycle>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(motorcycleSpecParams.Plate))
                filter &= builder.Where(m => m.Plate.ToUpper() == motorcycleSpecParams.Plate.ToUpper());

            return await _context
                .Motorcycles
                .Find(filter)
                .ToListAsync();
        }
    }
}
