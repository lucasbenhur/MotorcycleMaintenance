using Shared.AppLog.Data;
using Shared.AppLog.Entities;

namespace Shared.AppLog.Repositories
{
    public class AppLogRepository : IAppLogRepository
    {
        public IAppLogDbContext _context { get; }

        public AppLogRepository(IAppLogDbContext context)
        {
            _context = context;
        }

        public void CreateAsync(Log log)
        {
            _context.Logs.InsertOne(log);
        }
    }
}
