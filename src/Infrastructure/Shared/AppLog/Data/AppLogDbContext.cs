using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shared.AppLog.Entities;

namespace Shared.AppLog.Data
{
    public class AppLogDbContext : IAppLogDbContext
    {
        public IMongoCollection<Log> Logs { get; }

        public AppLogDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Logs = database.GetCollection<Log>(configuration["DatabaseSettings:LogsCollection"]);
        }
    }
}
