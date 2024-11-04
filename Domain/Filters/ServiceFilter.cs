namespace Domain.Filters;

public record ServiceFilter : BaseFilter
{
    public string? Name { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public int? DurationFrom { get; set; }
    public int? DurationTo { get; set; }
    public int? TrainerId { get; set; }
    public int? RoomId { get; set; }
    public int? CategoryId { get; set; }
}