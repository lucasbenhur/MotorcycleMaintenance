using MongoDB.Driver;
using Shared.Notifications.Entities;

namespace Shared.Notifications.Data
{
    public interface INotificationDbContext
    {
        IMongoCollection<Notification> Notifications { get; }
    }
}
