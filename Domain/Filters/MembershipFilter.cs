namespace Domain.Filters;

public record MembershipFilter : BaseFilter
{
    public int? MembershipTypeId { get; set; }
    public int? ClientId { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public DateTime? EndDateFrom { get; set; }
    public DateTime? EndDateTo { get; set; }
}