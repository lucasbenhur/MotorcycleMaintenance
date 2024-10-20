namespace Shared.Notifications.Responses
{
    public class NotificationResponse
    {
        public NotificationResponse(
            string id,
            string message)
        {
            Id = id;
            Message = message;
        }

        public string Id { get; internal set; }
        public string Message { get; internal set; }
    }
}
