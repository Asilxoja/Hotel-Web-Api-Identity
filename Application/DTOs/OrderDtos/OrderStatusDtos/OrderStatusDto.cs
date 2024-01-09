using Domain.Entities;

namespace Application.DTOs.OrderDtos.OrderStatusDtos;

public class OrderStatusDto : IdEntity
{
    public string Name { get; set; } = string.Empty;
}
