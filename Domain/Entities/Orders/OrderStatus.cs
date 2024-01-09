namespace Domain.Entities.Orders;

public class OrderStatus : IdEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Order> Orders { get; set; }
    = new List<Order>();
}
