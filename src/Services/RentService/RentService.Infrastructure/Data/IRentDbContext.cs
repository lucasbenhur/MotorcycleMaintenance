using MongoDB.Driver;
using RentService.Core.Entities;

namespace RentService.Infrastructure.Data
{
    public interface IRentDbContext
    {
        IMongoCollection<Rent> Rents { get; }
    }
}
