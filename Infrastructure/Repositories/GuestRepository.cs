using Domain.Entities.Guests;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class GuestRepository : Repository<Guest>, IGuestInterface
{
    public GuestRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
