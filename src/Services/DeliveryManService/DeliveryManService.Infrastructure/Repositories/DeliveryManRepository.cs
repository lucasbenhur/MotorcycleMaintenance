using DeliveryManService.Core.Entities;
using DeliveryManService.Core.Repositories;
using DeliveryManService.Infrastructure.Data;

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
    }
}
