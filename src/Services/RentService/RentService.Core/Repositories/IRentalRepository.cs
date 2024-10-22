using RentService.Core.Entities;

namespace RentalService.Core.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> CreateAsync(Rental rental);
    }
}
