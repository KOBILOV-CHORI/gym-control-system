namespace Domain.Dtos;

public record ClientUpdateDto : BaseClientDto
{
    public int Id { get; set; }
}

public record ClientCreateDto : BaseClientDto {}

public record ClientReadDto : BaseClientDto
{
    public int Id { get; set; }
}

public record BaseClientDto
{
    public bool IsActive { get; set; } = true;
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfileImage { get; set; }
}