using RentalService.Core.Repositories;
using RentService.Core.Entities;
using RentService.Infrastructure.Data;

namespace RentalService.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        public IRentalDbContext _context { get; }

        public RentalRepository(IRentalDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> CreateAsync(Rental rental)
        {
            await _context.Rentals.InsertOneAsync(rental);
            return rental;
        }
    }
}
