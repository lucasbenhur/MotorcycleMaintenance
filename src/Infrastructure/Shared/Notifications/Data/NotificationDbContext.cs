using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shared.Notifications.Entities;

namespace Shared.Notifications.Data
{
    public class NotificationDbContext : INotificationDbContext
    {
        public IMongoCollection<Notification> Notifications { get; }

        public NotificationDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Notifications = database.GetCollection<Notification>(configuration["DatabaseSettings:NotificationsCollection"]);
        }
    }
}
