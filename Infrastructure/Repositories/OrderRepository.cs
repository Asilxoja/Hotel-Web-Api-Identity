using Domain.Entities.Orders;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderInterface
{
    public OrderRepository(ApplicationDbContext dbContext): base(dbContext)
        
    {
    }
}
