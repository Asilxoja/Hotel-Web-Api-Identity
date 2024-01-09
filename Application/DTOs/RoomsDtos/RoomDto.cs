using Domain.Entities;

namespace Application.DTOs.RoomsDtos;

public class RoomDto : IdEntity
{
    public int Number { get; set; }
    public int Price { get; set; }
    public int RoomTypeId { get; set; }
    public int RoomStatusId { get; set; }
}
