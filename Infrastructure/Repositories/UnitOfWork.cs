
using Infrastructure.Data;
using Infrastructure.Interfaces;
using System;

public class UnitOfWork(ApplicationDbContext dbContext,
                       IStaffInterface staffInterface,
                       IGuestInterface guestInterface,
                       IOrderInterface orderInterface,
                       IOrderStatusInterface orderStatusInterface,
                       IRoomInterface roomInterface,
                       IRoomStatusInterface roomStatusInterface,
                       IRoomTypeInterface roomTypeInterface) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public IStaffInterface StaffInterface { get; } = staffInterface;

    public IGuestInterface GuestInterface { get; } = guestInterface;

    public IOrderStatusInterface OrderStatusInterface { get; } = orderStatusInterface;

    public IOrderInterface OrderInterface { get; } = orderInterface;

    public IRoomInterface RoomInterface { get; } = roomInterface;

    public IRoomStatusInterface RoomStatusInterface { get; } = roomStatusInterface;

    public IRoomTypeInterface RoomTypeInterface { get; } = roomTypeInterface;

    public void Dispose()
         => GC.SuppressFinalize(this);

    public async Task SaveAsync()
            => await _dbContext.SaveChangesAsync();
}