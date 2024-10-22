using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RentService.Core.Entities;
using RentService.Infrastructure.Data;

namespace RentalService.Infrastructure.Data
{
    public class RentalDbContext : IRentalDbContext
    {
        public IMongoCollection<Rental> Rentals { get; }

        public RentalDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Rentals = database.GetCollection<Rental>(configuration["DatabaseSettings:RentalsCollection"]);
        }
    }
}
