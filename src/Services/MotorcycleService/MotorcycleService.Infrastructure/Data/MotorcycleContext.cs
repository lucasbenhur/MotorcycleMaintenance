using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MotorcycleService.Core.Entities;

namespace MotorcycleService.Infrastructure.Data
{
    public class MotorcycleContext : IMotorcycleContext
    {
        public IMongoCollection<Motorcycle> Motorcycles { get; }

        public MotorcycleContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Motorcycles = database.GetCollection<Motorcycle>(configuration["DatabaseSettings:MotorcyclesCollection"]);

            // Criando um índice único na placa
            var indexKeys = Builders<Motorcycle>.IndexKeys.Ascending(m => m.Plate);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Motorcycle>(indexKeys, indexOptions);

            Motorcycles.Indexes.CreateOne(indexModel);
        }
    }
}
