using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RentService.Core.Entities;

namespace RentService.Infrastructure.Data
{
    public class RentDbContext : IRentDbContext
    {
        public IMongoCollection<Rent> Rents { get; }

        public RentDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Rents = database.GetCollection<Rent>(configuration["DatabaseSettings:RentsCollection"]);
        }
    }
}
