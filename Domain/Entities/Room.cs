namespace Domain.Entities;

public class Room : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }
    public bool Availability { get; set; }
    public int MembershipTypeId { get; set; }
    
    public MembershipType MembershipType { get; set; }
    public List<Class> Classes { get; set; } = new List<Class>();
    public List<Service> Services { get; set; } = new List<Service>();
    public List<Equipment> Equipments { get; set; } = new List<Equipment>();
}