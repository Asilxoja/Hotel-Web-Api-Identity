using Application.DTOs.OrderDtos.OrderStatusDtos;

namespace Application.Interfaces;

public interface IOrderStatusService
{
    Task<List<OrderStatusDto>> GetAllAsync();
    Task<OrderStatusDto> GetByIdAsync(int id);
    Task Add(AddOrderStatusDto orderStatus);
    Task Update(UpdateOrderStatusDto orderStatus);
    Task Delete(int id);
}
