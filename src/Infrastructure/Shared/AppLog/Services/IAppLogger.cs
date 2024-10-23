namespace Shared.AppLog.Services
{
    public interface IAppLogger
    {
        void LogInformation(string message);
        void LogError(Exception exception, string message);
    }
}
