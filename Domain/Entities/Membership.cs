namespace Domain.Entities;

public class Membership : BaseEntity
{
    public int MembershipTypeId { get; set; }
    public int ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
    
    public Client Client { get; set; }
    public MembershipType MembershipType { get; set; }
}