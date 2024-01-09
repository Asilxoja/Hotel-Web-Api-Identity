using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Rooms;

public class Room : IdEntity
{
    public int Number { get; set; }
    public int Price { get; set; }

    [ForeignKey("RoomType")]
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; } = new();

    [ForeignKey("RoomStatus")]
    public int RoomStatusId { get; set; }
    public RoomStatus RoomStatus { get; set; } = new();
}
