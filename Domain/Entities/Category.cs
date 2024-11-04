namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    
    public List<Class> Classes { get; set; } = new List<Class>();
    public List<Service> Services { get; set; } = new List<Service>();
    public List<MembershipType> MembershipTypes { get; set; }
}