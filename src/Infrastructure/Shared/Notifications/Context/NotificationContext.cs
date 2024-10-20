using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shared.Notifications.Entities;

namespace Shared.Notifications.Context
{
    public class NotificationContext : INotificationContext
    {
        public IMongoCollection<Notification> Notifications { get; }

        public NotificationContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Notifications = database.GetCollection<Notification>(configuration["DatabaseSettings:NotificationsCollection"]);
        }
    }
}
