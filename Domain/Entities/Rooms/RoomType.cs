namespace Domain.Entities.Rooms;

public class RoomType : IdEntity
{
    public string Name { get; set; } = string.Empty;

    public int PersonCount { get; set; }
    public ICollection<Room> Rooms { get; set; } 
        = new List<Room>();
}
