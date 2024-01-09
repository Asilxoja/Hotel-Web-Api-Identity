using Domain.Entities.Rooms;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class RoomStatusRepository : Repository<RoomStatus>, IRoomStatusInterface
{
    public RoomStatusRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
