using DeliveryManService.Core.Entities;
using DeliveryManService.Core.Repositories;
using DeliveryManService.Core.Specs;
using DeliveryManService.Infrastructure.Data;
using MongoDB.Driver;

namespace DeliveryManService.Infrastructure.Repositories
{
    public class DeliveryManRepository : IDeliveryManRepository
    {
        private readonly IDeliveryManDbContext _context;

        public DeliveryManRepository(
            IDeliveryManDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryMan> CreateAsync(DeliveryMan deliveryMan)
        {
            await _context.DeliveryMen.InsertOneAsync(deliveryMan);
            return deliveryMan;
        }

        public async Task<ICollection<DeliveryMan>> GetAllAsync(GetAllDeliveryMenSpecParams specParams)
        {
            var builder = Builders<DeliveryMan>.Filter;
            var filters = new List<FilterDefinition<DeliveryMan>>();

            if (!string.IsNullOrWhiteSpace(specParams.Id))
                filters.Add(builder.Where(m => m.Id.ToUpper() == specParams.Id.ToUpper()));

            if (!string.IsNullOrWhiteSpace(specParams.Cnpj))
                filters.Add(builder.Where(m => m.Cnpj == specParams.Cnpj));

            if (!string.IsNullOrWhiteSpace(specParams.CnhNumber))
                filters.Add(builder.Where(m => m.CnhNumber == specParams.CnhNumber));

            var filter = filters.Count > 0 ? builder.And(filters) : builder.Empty;

            return await _context
                .DeliveryMen
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(DeliveryMan deliveryMan)
        {
            var updatedMotorcycle = await _context
                .DeliveryMen
                .ReplaceOneAsync(m => m.Id.ToUpper() == deliveryMan.Id.ToUpper(), deliveryMan);

            return updatedMotorcycle.IsAcknowledged;
        }

        public async Task<DeliveryMan> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return await _context
                .DeliveryMen
                .Find(m => m.Id.ToUpper() == id.ToUpper())
                .FirstOrDefaultAsync();
        }
    }
}
