using MongoDB.Driver;
using RentService.Core.Entities;

namespace RentService.Infrastructure.Data
{
    public interface IRentalDbContext
    {
        IMongoCollection<Rental> Rentals { get; }
    }
}
