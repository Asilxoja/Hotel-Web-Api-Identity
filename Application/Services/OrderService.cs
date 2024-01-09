using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.DTOs.OrderDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Orders;
using Infrastructure.Interfaces;

namespace Application.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddOrder(AddOrderDto addorderDto)
    {
        if (addorderDto == null)
        {
            throw new ArgumentNullException(nameof(addorderDto));
        }
        var order = _mapper.Map<Order>(addorderDto);
        var orders = await _unitOfWork.OrderInterface.GetAllAsync();
        if (order.IsExist(orders))
        {
            throw new CustomException($"{order} - order already added");
        }
        try
        {
            await _unitOfWork.OrderInterface.AddAsync(order);
            await _unitOfWork.SaveAsync();
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteOrder(int id)
    {
        var order = await _unitOfWork.OrderInterface.GetByIdAsync(id);
        if (order == null)
        {
            throw new ArgumentNullException();
        }
        await _unitOfWork.OrderInterface.DeleteAsync(order);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<OrderDto>> GetAll()
    {
        var orders = await _unitOfWork.OrderInterface.GetAllAsync();
        return orders.Select(o => _mapper.Map<OrderDto>(o)).ToList();
    }

    public async Task<OrderDto> GetById(int id)
    {
        var order = await _unitOfWork.OrderInterface.GetByIdAsync(id);
        if (order == null)
        {
            throw new ArgumentNullException();
        }
        return _mapper.Map<OrderDto>(order);
    }

    public async Task UpdateOrder(UpdateOrderDto updateorderDto)
    {
        if (updateorderDto == null)
        {
            throw new ArgumentNullException(nameof(updateorderDto));
        }
        var order = _mapper.Map<Order>(updateorderDto);
        var orders = await _unitOfWork.OrderInterface.GetAllAsync();
        if (order.IsExist(orders))
        {
            throw new CustomException($"{order} - order already added");
        }
        try
        {
            await _unitOfWork.OrderInterface.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

