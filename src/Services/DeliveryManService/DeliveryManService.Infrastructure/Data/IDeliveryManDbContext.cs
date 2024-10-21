using DeliveryManService.Core.Entities;
using MongoDB.Driver;

namespace DeliveryManService.Infrastructure.Data
{
    public interface IDeliveryManDbContext
    {
        IMongoCollection<DeliveryMan> DeliveryMen { get; }
    }
}
