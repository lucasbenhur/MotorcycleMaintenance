using MongoDB.Driver;
using MotorcycleService.Core.Entities;

namespace MotorcycleService.Infrastructure.Data
{
    public interface IMotorcycleDbContext
    {
        IMongoCollection<Motorcycle> Motorcycles { get; }
    }
}
