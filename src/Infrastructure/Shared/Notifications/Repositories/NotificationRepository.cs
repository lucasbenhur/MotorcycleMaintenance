using Shared.Notifications.Data;
using Shared.Notifications.Entities;

namespace Shared.Notifications.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public INotificationDbContext _context { get; }

        public NotificationRepository(INotificationDbContext context)
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
