namespace Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public int? TrainerId { get; set; }
    public int CategoryId { get; set; }
    public int? RoomId { get; set; }
    
    public Trainer Trainer { get; set; }
    public Room Room { get; set; }
    public Category Category { get; set; }
}