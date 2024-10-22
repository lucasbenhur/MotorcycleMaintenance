using RentService.Core.Repositories;
using RentService.Core.Entities;
using RentService.Infrastructure.Data;

namespace RentService.Infrastructure.Repositories
{
    public class RentRepository : IRentRepository
    {
        public IRentDbContext _context { get; }

        public RentRepository(IRentDbContext context)
        {
            _context = context;
        }

        public async Task<Rent> CreateAsync(Rent rent)
        {
            await _context.Rents.InsertOneAsync(rent);
            return rent;
        }
    }
}
