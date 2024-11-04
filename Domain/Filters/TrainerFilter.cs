namespace Domain.Filters;

public record TrainerFilter : BaseFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Specialization { get; set; }
    public int? ExperienceYearsFrom { get; set; }
    public int? ExperienceYearsTo { get; set; }
    public int? RatingFrom { get; set; }
    public int? RatingTo { get; set; }
}