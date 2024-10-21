using DeliveryManService.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DeliveryManService.Infrastructure.Data
{
    public class DeliveryManDbContext : IDeliveryManDbContext
    {
        public IMongoCollection<DeliveryMan> DeliveryMen { get; }

        public DeliveryManDbContext(
            IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            DeliveryMen = database.GetCollection<DeliveryMan>(configuration["DatabaseSettings:DeliveryMenCollection"]);
            CreateCnpjIndex();
            CreateCnhNumberIndex();
        }

        private void CreateCnpjIndex()
        {
            var indexKeys = Builders<DeliveryMan>.IndexKeys.Ascending(m => m.Cnpj);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<DeliveryMan>(indexKeys, indexOptions);
            DeliveryMen.Indexes.CreateOne(indexModel);
        }

        private void CreateCnhNumberIndex()
        {
            var indexKeys = Builders<DeliveryMan>.IndexKeys.Ascending(m => m.CnhNumber);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<DeliveryMan>(indexKeys, indexOptions);
            DeliveryMen.Indexes.CreateOne(indexModel);
        }
    }
}
