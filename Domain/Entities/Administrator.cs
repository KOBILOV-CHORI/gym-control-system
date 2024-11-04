namespace Domain.Entities;

public class Administrator : User
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
}