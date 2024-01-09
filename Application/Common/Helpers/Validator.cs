using Domain.Entities.Guests;
using Domain.Entities.Identity;
using Domain.Entities.Orders;
using Domain.Entities.Rooms;
using Domain.Entities.Staffs;

namespace Application.Common.Helpers;


public static class Validator
{
    public static bool IsValid(this Staff staff)
        => !string.IsNullOrEmpty(staff.FirstName)
           && !string.IsNullOrEmpty(staff.LastName)
           && !string.IsNullOrEmpty(staff.FatherName)
           && !string.IsNullOrEmpty(staff.Gender)
           && staff.FirstName.Length >= 2
           && staff.FirstName.Length <= 50
           && staff.LastName.Length >= 2
           && staff.FirstName.Length <= 50;

    public static bool IsValid(this Guest guest)
        => !string.IsNullOrEmpty(guest.FirstName)
           && !string.IsNullOrEmpty(guest.LastName)
           && !string.IsNullOrEmpty(guest.FatherName)
           && !string.IsNullOrEmpty(guest.Gender)
           && guest.FirstName.Length >= 2
           && guest.FirstName.Length <= 50
           && guest.LastName.Length >= 2
           && guest.FirstName.Length <= 50;

    public static bool IsValid(this ApplicationUser admin)
        => admin != null
           && !string.IsNullOrEmpty(admin.FirstName)
           && !string.IsNullOrEmpty(admin.LastName)
           && !string.IsNullOrEmpty(admin.FatherName)
           && admin.FirstName.Length >= 2
           && admin.FirstName.Length <= 50
           && admin.LastName.Length >= 2
           && admin.FirstName.Length <= 50;

    public static bool IsValid(this Room room)
        => room != null;

    public static bool IsValid(this RoomStatus roomStatus)
        => roomStatus != null
        && !string.IsNullOrEmpty(roomStatus.Name);

    public static bool IsValid(this OrderStatus orderStatus)
        => orderStatus != null
        && !string.IsNullOrEmpty(orderStatus.Name);

    public static bool IsExist(this ApplicationUser admin, IEnumerable<ApplicationUser> admins)
        => admins.Any(a => a.FirstName == admin.FirstName
        && a.LastName == admin.LastName
        && a.FatherName == admin.FatherName);

    public static bool IsExist(this Room room, IEnumerable<Room> rooms)
        => room != null
        && rooms.Any(r => r.RoomType == room.RoomType
        && r.RoomStatus == room.RoomStatus
        && r.Number == room.Number);

    public static bool IsExist(this RoomType roomType, IEnumerable<RoomType> roomTypes)
        => roomType != null
        && roomTypes.Any(r => r.Name == roomType.Name);
    public static bool IsExist(this RoomStatus roomStatus, IEnumerable<RoomStatus> roomstatuss)
        => roomStatus != null && roomstatuss.Any(r => r.Name == roomStatus.Name && r.Id == roomStatus.Id);

    public static bool IsExist(this Order order, IEnumerable<Order> orders)
        => order != null
        && orders.Any(o => o.StartDate == order.StartDate
        && o.EndDate == order.EndDate);

    public static bool IsExist(this OrderStatus orderStatus, IEnumerable<OrderStatus> orders)
        => orderStatus != null && orders.Any(o => o.Id == orderStatus.Id
        && o.Name == orderStatus.Name);

    public static bool IsExist(this Guest guest, IEnumerable<Guest> guests)
        => guest != null
        && guests.Any(g => g.BirthDate == guest.BirthDate
        && g.Passport == guest.Passport
        && g.Address == guest.Address
        && g.Citizenship == guest.Citizenship
        && g.PhoneNumber == guest.PhoneNumber
        && g.OrganizationName == guest.OrganizationName
        && g.LastName == guest.LastName
        && g.FatherName == guest.FirstName
        );
}
