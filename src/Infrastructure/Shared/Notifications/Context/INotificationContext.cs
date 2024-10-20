using MongoDB.Driver;
using Shared.Notifications.Entities;

namespace Shared.Notifications.Context
{
    public interface INotificationContext
    {
        IMongoCollection<Notification> Notifications { get; }
    }
}
