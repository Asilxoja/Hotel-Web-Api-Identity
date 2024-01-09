using Domain.Entities;

namespace Application.DTOs.RoomDtos.RoomStatusDtos;

public class UpdateRoomStatusDto : IdEntity
{
    public string Name { get; set; } = string.Empty;
}
