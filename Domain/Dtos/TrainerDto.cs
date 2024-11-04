namespace Domain.Dtos;

public record TrainerUpdateDto : BaseTrainerDto
{
    public int Id { get; set; }
}

public record TrainerCreateDto : BaseTrainerDto {}

public record TrainerReadDto : BaseTrainerDto
{
    public int Id { get; set; }
}

public record BaseTrainerDto
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string? ProfileImage { get; set; }
    public string Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public int Rating { get; set; }
}