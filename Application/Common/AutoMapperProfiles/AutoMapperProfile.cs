using Application.DTOs.GuestDtos;
using Application.DTOs.OrderDtos.OrderStatusDtos;
using Application.DTOs.OrderDtos;
using Application.DTOs.RoomDtos.RoomStatusDtos;
using Application.DTOs.RoomsDtos.RoomStatusDtos;
using Application.DTOs.RoomsDtos.RoomTypeDtos;
using Application.DTOs.RoomsDtos;
using Application.DTOs.StaffDtos;
using AutoMapper;
using Domain.Entities.Guests;
using Domain.Entities.Orders;
using Domain.Entities.Rooms;
using Domain.Entities.Staffs;
namespace Application.Common.AutoMapperProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Staff Dto
        CreateMap<AddStaffDto, Staff>();
        CreateMap<StaffDto, Staff>().ReverseMap();
        CreateMap<UpdateStaffDto, Staff>();
        #endregion

        #region Guest Dto
        CreateMap<AddGuestDto, Guest>();
        CreateMap<GuestDto, Guest>().ReverseMap();
        CreateMap<UpdateGuestDto, Guest>();
        #endregion

        #region Room Dto
        CreateMap<AddRoomDto, Room>();
        CreateMap<RoomDto, Room>().ReverseMap();
        CreateMap<UpdateRoomDto, Room>();
        #endregion

        #region Room Status Dto
        CreateMap<AddRoomStatusDto, RoomStatus>();
        CreateMap<RoomStatusDto, RoomStatus>().ReverseMap();
        CreateMap<UpdateRoomStatusDto, RoomStatus>();
        #endregion

        #region Room Type Dto
        CreateMap<AddRoomTypeDto, RoomType>();
        CreateMap<RoomTypeDto, RoomType>().ReverseMap();
        CreateMap<UpdateRoomTypeDto, RoomType>();
        #endregion

        #region Order
        CreateMap<AddOrderDto, Order>();
        CreateMap<Order, OrderDto>()
            .ReverseMap();
        CreateMap<UpdateOrderDto, Order>();
        #endregion

        #region Order Status
        CreateMap<AddOrderStatusDto, OrderStatus>();
        CreateMap<OrderStatusDto, OrderStatus>()
            .ReverseMap();
        CreateMap<UpdateOrderStatusDto, OrderStatus>();
        #endregion
    }
}
