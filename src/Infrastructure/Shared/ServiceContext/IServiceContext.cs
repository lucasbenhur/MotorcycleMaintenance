namespace Shared.ServiceContext
{
    public interface IServiceContext
    {
        string? Notification { get; }

        void AddNotification(string notification);
        void ClearNotification();
        bool HasNotification();
    }
}
