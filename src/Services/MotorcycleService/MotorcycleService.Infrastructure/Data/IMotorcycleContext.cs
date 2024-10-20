using MongoDB.Driver;
using MotorcycleService.Core.Entities;

namespace MotorcycleService.Infrastructure.Data
{
    public interface IMotorcycleContext
    {
        IMongoCollection<Motorcycle> Motorcycles { get; }
    }
}
