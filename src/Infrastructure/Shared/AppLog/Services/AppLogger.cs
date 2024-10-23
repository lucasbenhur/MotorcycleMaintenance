using Microsoft.Extensions.Logging;
using Shared.AppLog.Entities;
using Shared.AppLog.Repositories;

namespace Shared.AppLog.Services
{
    public class AppLogger : IAppLogger
    {
        private readonly IAppLogRepository _appLogRepository;
        private readonly ILogger<AppLogger> _logger;

        public AppLogger(
            IAppLogRepository appLogRepository,
            ILogger<AppLogger> logger)
        {
            _appLogRepository = appLogRepository;
            _logger = logger;
        }

        public void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
            var log = new Log(LogLevel.Error.ToString(), message, exception);
            _appLogRepository.CreateAsync(log);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
            var log = new Log(LogLevel.Information.ToString(), message);
            _appLogRepository.CreateAsync(log);
        }
    }
}
