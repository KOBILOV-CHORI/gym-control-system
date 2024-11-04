namespace Domain.Entities;

public class Client : User
{
    public bool IsActive { get; set; } = true;
    
    public List<Membership> Memberships { get; set; } = new List<Membership>();
    public List<Booking> Bookings { get; set; } = new List<Booking>();
}