namespace Domain.Entities;

public class MembershipType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public int? CategoryId { get; set; }
    
    public Category Category { get; set; }
    public List<Membership> Memberships { get; set; }
    public List<Room> Rooms { get; set; }
}