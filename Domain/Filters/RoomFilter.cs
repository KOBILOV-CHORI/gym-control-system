namespace Domain.Filters;

public record RoomFilter : BaseFilter
{
    public string Name { get; set; }
    public int? CapacityFrom { get; set; }
    public int? CapacityTo { get; set; }
    public string Location { get; set; }
    public bool? Availability { get; set; }
    public int? MembershipTypeId { get; set; }
}