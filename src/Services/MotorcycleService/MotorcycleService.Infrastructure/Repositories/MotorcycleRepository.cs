using MongoDB.Driver;
using MotorcycleService.Core.Entities;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Core.Specs;
using MotorcycleService.Infrastructure.Data;

namespace MotorcycleService.Infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        public IMotorcycleDbContext _context { get; }

        public MotorcycleRepository(IMotorcycleDbContext context)
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

        public async Task<ICollection<Motorcycle>> GetAllAsync(GetAllMotorcyclesSpecParams specParams)
        {
            var builder = Builders<Motorcycle>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(specParams.Plate))
                filter &= builder.Where(m => m.Plate.ToUpper() == specParams.Plate.ToUpper());

            return await _context
                .Motorcycles
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Motorcycle motorcycle)
        {
            var updatedMotorcycle = await _context
                .Motorcycles
                .ReplaceOneAsync(m => m.Id.ToUpper() == motorcycle.Id.ToUpper(), motorcycle);

            return updatedMotorcycle.IsAcknowledged;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deletedMotorcycle = await _context
                .Motorcycles
                .DeleteOneAsync(m => m.Id.ToUpper() == id.ToUpper());

            return deletedMotorcycle.IsAcknowledged;
        }
    }
}
