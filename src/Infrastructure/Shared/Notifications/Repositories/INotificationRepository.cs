using Shared.Notifications.Entities;

namespace Shared.Notifications.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);
    }
}
