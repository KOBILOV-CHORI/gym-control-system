namespace Domain.Dtos;

public record RoleUpdateDto : BaseRoleDto
{
    public int Id { get; set; }
}

public record RoleCreateDto : BaseRoleDto {}

public record RoleReadDto : BaseRoleDto
{
    public int Id { get; set; }
}

public record BaseRoleDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
}