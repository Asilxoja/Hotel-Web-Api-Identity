using Domain.Entities;

namespace Application.DTOs.RoomsDtos.RoomTypeDtos;

public class UpdateRoomTypeDto : IdEntity
{
    public string Name { get; set; } = string.Empty;
    public int PersonCount { get; set; }
}
