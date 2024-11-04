namespace Domain.Entities;

public class Booking : BaseEntity
{
    public int ClientId { get; set; }
    public int ClassId { get; set; }
    
    public Client Client { get; set; }
    public Class Class { get; set; }
}