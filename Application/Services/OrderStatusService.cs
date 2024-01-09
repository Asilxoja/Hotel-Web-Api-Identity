using Application.Common.Exceptions;
using Application.Common.Helpers;
using Application.DTOs.OrderDtos.OrderStatusDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Orders;
using Infrastructure.Interfaces;

namespace Application.Services;

public class OrderStatusServices(IUnitOfWork unitOfWork, IMapper mapper) : IOrderStatusService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Add(AddOrderStatusDto orderStatus)
    {
        if (orderStatus == null)
        {
            throw new ArgumentNullException();
        }
        var orderSt = _mapper.Map<OrderStatus>(orderStatus);
        var orderstatus = await _unitOfWork.OrderStatusInterface.GetAllAsync();
        if (orderSt.IsExist(orderstatus))
        {
            throw new CustomException($"{orderStatus} - order already added");
        }
        try
        {
            await _unitOfWork.OrderStatusInterface.AddAsync(orderSt);
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

    public async Task Delete(int id)
    {
        var orderstatus = _unitOfWork.OrderStatusInterface.GetByIdAsync(id);
        var order = await orderstatus;
        if (order == null)
        {
            throw new ArgumentNullException("Id is null");
        }
        await _unitOfWork.OrderStatusInterface.DeleteAsync(order);
        await _unitOfWork.SaveAsync();
    }

    public async Task<List<OrderStatusDto>> GetAllAsync()
    {
        var orderstatuss = await _unitOfWork.OrderStatusInterface.GetAllAsync();
        return orderstatuss.Select(o => _mapper.Map<OrderStatusDto>(o)).ToList();
    }

    public async Task<OrderStatusDto> GetByIdAsync(int id)
    {
        var orderstatus = await _unitOfWork.OrderStatusInterface.GetByIdAsync(id);
        if (orderstatus == null)
        {
            throw new ArgumentNullException();
        }
        return _mapper.Map<OrderStatusDto>(orderstatus);
    }

    public async Task Update(UpdateOrderStatusDto orderStatus)
    {
        try
        {
            var order1 = _mapper.Map<OrderStatus>(orderStatus);
            var orders = await _unitOfWork.OrderStatusInterface.GetAllAsync();
            if (order1.IsExist(orders))
            {
                throw new CustomException($"{order1.Name} already Exist!");
            }
            if (orderStatus is null)
            {
                throw new ArgumentNullException(nameof(orderStatus), "Updated orderstatus information is null");
            }

            var order = await _unitOfWork.OrderStatusInterface.GetByIdAsync(orderStatus.Id);
            if (order is null)
            {
                throw new ArgumentException($"No admin found with Id {orderStatus.Id}", nameof(orderStatus.Id));
            }
            _mapper.Map(orderStatus, order);

            if (!order.IsValid())
            {
                throw new CustomException("admin information is invalid!");
            }

            await _unitOfWork.OrderStatusInterface.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
