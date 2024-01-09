using Domain.Entities.Rooms;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class RoomRepository : Repository<Room>, IRoomInterface
{
    public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
