using DeliveryManService.Core.Entities;

namespace DeliveryManService.Core.Repositories
{
    public interface IDeliveryManRepository
    {
        Task<DeliveryMan> CreateAsync(DeliveryMan deliveryMan);
    }
}
