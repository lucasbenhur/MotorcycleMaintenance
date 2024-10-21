using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MotorcycleService.Core.Entities;

namespace MotorcycleService.Infrastructure.Data
{
    public class MotorcycleDbContext : IMotorcycleDbContext
    {
        public IMongoCollection<Motorcycle> Motorcycles { get; }

        public MotorcycleDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Motorcycles = database.GetCollection<Motorcycle>(configuration["DatabaseSettings:MotorcyclesCollection"]);
            CreatePlateIndex();
        }

        private void CreatePlateIndex()
        {
            var indexKeys = Builders<Motorcycle>.IndexKeys.Ascending(m => m.Plate);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Motorcycle>(indexKeys, indexOptions);
            Motorcycles.Indexes.CreateOne(indexModel);
        }
    }
}
