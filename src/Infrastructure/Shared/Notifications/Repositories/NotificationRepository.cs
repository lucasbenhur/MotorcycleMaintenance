using Shared.Notifications.Context;
using Shared.Notifications.Entities;

namespace Shared.Notifications.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public INotificationContext _context { get; }

        public NotificationRepository(INotificationContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            await _context.Notifications.InsertOneAsync(notification);
            return notification;
        }
    }
}
