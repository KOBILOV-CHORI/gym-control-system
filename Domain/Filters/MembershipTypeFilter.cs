namespace Domain.Filters;

public record MembershipTypeFilter : BaseFilter
{
    public string? Name { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public int? DurationInDaysFrom { get; set; }
    public int? DurationInDaysTo { get; set; }
    public int? CategoryId { get; set; }
}