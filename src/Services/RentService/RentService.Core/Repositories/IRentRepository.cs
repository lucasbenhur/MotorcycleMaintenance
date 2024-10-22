using RentService.Core.Entities;

namespace RentService.Core.Repositories
{
    public interface IRentRepository
    {
        Task<Rent> CreateAsync(Rent rent);
    }
}
