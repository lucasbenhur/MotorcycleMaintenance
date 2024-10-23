using MongoDB.Driver;
using Shared.AppLog.Entities;

namespace Shared.AppLog.Data
{
    public interface IAppLogDbContext
    {
        IMongoCollection<Log> Logs { get; }
    }
}
