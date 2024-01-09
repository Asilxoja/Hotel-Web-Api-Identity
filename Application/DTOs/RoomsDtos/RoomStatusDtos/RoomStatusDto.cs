using Domain.Entities;

namespace Application.DTOs.RoomDtos.RoomStatusDtos;

public class RoomStatusDto : IdEntity
{
    public string Name { get; set; } = string.Empty;
}
