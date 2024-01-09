using Domain.Entities.Staffs;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;


public class StaffRepository : Repository<Staff>, IStaffInterface
{
    public StaffRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}
