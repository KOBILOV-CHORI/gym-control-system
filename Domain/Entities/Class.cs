namespace Domain.Entities;

public class Class : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
    public string Level { get; set; }
    public int FrequencyPerWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TrainerId { get; set; }
    public int RoomId { get; set; }
    public int CategoryId { get; set; }
    
    public Trainer Trainer { get; set; }
    public Room Room { get; set; }
    public Category Category { get; set; }
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}