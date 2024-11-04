namespace Domain.Entities;

public class Equipment : BaseEntity
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public int RoomId { get; set; }
    
    public Room Room { get; set; }
}