namespace Domain.Filters;

public record EquipmentFilter : BaseFilter
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int? QuantityFrom { get; set; }
    public int? QuantityTo { get; set; }
    public int? RoomId { get; set; }
}