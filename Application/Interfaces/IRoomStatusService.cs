using Application.DTOs.RoomDtos.RoomStatusDtos;

using Application.DTOs.RoomsDtos.RoomStatusDtos;

namespace Application.Interfaces;

public interface IRoomStatusService
{
    Task<List<RoomStatusDto>> GetAllRoomStatus();
    Task<RoomStatusDto> GetRoomStatusById(int Id);
    Task AddRoomStatus(AddRoomStatusDto roomStatusDto);
    Task UpdateRoomStatus(UpdateRoomStatusDto roomStatusDto);
    Task DeleteRoomStatus(int Id);
}
