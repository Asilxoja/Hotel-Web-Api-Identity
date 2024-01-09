namespace Domain.Entities.Rooms;

public class RoomStatus : IdEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
