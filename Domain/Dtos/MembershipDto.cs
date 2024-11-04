namespace Domain.Dtos;

public record MembershipUpdateDto : BaseMembershipDto
{
    public int Id { get; set; }
}

public record MembershipCreateDto : BaseMembershipDto {}

public record MembershipReadDto : BaseMembershipDto
{
    public int Id { get; set; }
}

public record BaseMembershipDto
{
    public int MembershipTypeId { get; set; }
    public int ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
}