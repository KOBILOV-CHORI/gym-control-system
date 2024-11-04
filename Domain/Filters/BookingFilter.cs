namespace Domain.Filters;

public record BookingFilter : BaseFilter
{
    public int? ClientId { get; set; }
    public int? ClassId { get; set; }
    public DateTime? BookingDateFrom { get; set; }
    public DateTime? BookingDateTo { get; set; }
}