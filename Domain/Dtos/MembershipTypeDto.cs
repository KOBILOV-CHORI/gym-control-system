namespace Domain.Dtos;

public record MembershipTypeUpdateDto : BaseMembershipTypeDto
{
    public int Id { get; set; }
}

public record MembershipTypeCreateDto : BaseMembershipTypeDto {}

public record MembershipTypeReadDto : BaseMembershipTypeDto
{
    public int Id { get; set; }
}

public record BaseMembershipTypeDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
    public int? CategoryId { get; set; }
}