using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Notifications.Command;
using Shared.Notifications.Entities;
using Shared.Notifications.Repositories;
using Shared.Notifications.Responses;

namespace Shared.Notifications.Handlers
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<CreateNotificationCommandHandler> _logger;

        public CreateNotificationCommandHandler(
            INotificationRepository notificationRepository,
            ILogger<CreateNotificationCommandHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public async Task<NotificationResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notificationEntity = new Notification(request.Message);
                var newNotification = await _notificationRepository.CreateAsync(notificationEntity);
                _logger.LogInformation("Notificação armazenada no banco de dados para consulta futura com Id {Id}", newNotification.Id);
                return new NotificationResponse(newNotification.Id.ToString(), newNotification.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao gravar a notificação");
                return null;
            }
        }
    }
}
