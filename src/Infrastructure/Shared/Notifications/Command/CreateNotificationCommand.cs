using MediatR;
using Shared.Notifications.Responses;

namespace Shared.Notifications.Command
{
    public class CreateNotificationCommand : IRequest<NotificationResponse>
    {
        public CreateNotificationCommand(
            string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
