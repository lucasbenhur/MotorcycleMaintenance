using MongoDB.Bson;
using MongoDB.Driver;
using RentService.Core.Entities;
using RentService.Core.Repositories;
using RentService.Infrastructure.Data;

namespace RentService.Infrastructure.Repositories
{
    public class RentRepository : IRentRepository
    {
        public IRentDbContext _context { get; }

        public RentRepository(IRentDbContext context)
        {
            _context = context;
        }

        public async Task<Rent> CreateAsync(Rent rent)
        {
            await _context.Rents.InsertOneAsync(rent);
            return rent;
        }

        public async Task<Rent?> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            if (ObjectId.TryParse(id, out ObjectId objectId))
            {
                return await _context
                    .Rents
                    .Find(m => m.Id == objectId)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<Rent?> GetMotorcycleRentAsync(string motorcycleId)
        {
            return await _context
                .Rents
                .Find(m => m.MotorcycleId.ToUpper() == motorcycleId.ToUpper())
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(Rent rent)
        {
            var updatedRent = await _context
            .Rents
            .ReplaceOneAsync(m => m.Id == rent.Id, rent);

            return updatedRent.IsAcknowledged;
        }
    }
}
