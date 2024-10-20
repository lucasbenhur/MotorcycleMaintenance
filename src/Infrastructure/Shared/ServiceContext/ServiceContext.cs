namespace Shared.ServiceContext
{
    public class ServiceContext : IServiceContext
    {
        public ServiceContext()
        {
            Notification = null;
        }

        public ServiceContext(string notification)
        {
            Notification = notification;
        }

        public string Notification { get; internal set; }

        public void AddNotification(string notification)
        {
            if (!string.IsNullOrWhiteSpace(Notification))
                Notification += " | " + notification;
            else
                Notification = notification;
        }

        public void ClearNotification() => Notification = null;

        public bool HasNotification() => !string.IsNullOrWhiteSpace(Notification);
    }
}
