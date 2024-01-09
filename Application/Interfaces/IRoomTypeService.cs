using Application.DTOs.RoomsDtos.RoomTypeDtos;

namespace Application.Interfaces;

public interface IRoomTypeService
{
    Task<List<RoomTypeDto>> GetAll();
    Task<RoomTypeDto> GetById(int id);
    Task AddRoomType(AddRoomTypeDto addRoomTypeDto);
    Task UpdateRoomType(UpdateRoomTypeDto updateRoomTypeDto);
    Task DeleteRoomType(int id);
}