using Shared.AppLog.Entities;

namespace Shared.AppLog.Repositories
{
    public interface IAppLogRepository
    {
        void CreateAsync(Log notification);
    }
}
