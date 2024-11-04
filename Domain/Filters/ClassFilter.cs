namespace Domain.Filters;

public record ClassFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Level { get; set; }
    public int? DurationFrom { get; set; }
    public int? DurationTo { get; set; }
    public int? CapacityFrom { get; set; }
    public int? CapacityTo { get; set; }
    public int? FrequencyPerWeekFrom { get; set; }
    public int? FrequencyPerWeekTo { get; set; }
    public int? TrainerId { get; set; }
    public int? RoomId { get; set; }
    public int? CategoryId { get; set; }
}