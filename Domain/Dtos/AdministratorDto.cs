using Domain.Entities;

namespace Domain.Dtos;

public record AdministratorUpdateDto : BaseAdministratorDto
{
    public int Id { get; set; }
}

public record AdministratorCreateDto : BaseAdministratorDto
{
}

public record AdministratorReadDto : BaseAdministratorDto
{
    public int Id { get; set; }
}

public record BaseAdministratorDto
{
    public int RoleId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfileImage { get; set; }
}