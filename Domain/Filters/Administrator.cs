using Domain.Filters;

namespace Domain.Filters;

public record AdministratorFilter : BaseFilter
{
    public int? RoleId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}