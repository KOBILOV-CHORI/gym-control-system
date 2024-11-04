namespace Domain.Dtos;

public record RoomUpdateDto : BaseRoomDto
{
    public int Id { get; set; }
}

public record RoomCreateDto : BaseRoomDto {}

public record RoomReadDto : BaseRoomDto
{
    public int Id { get; set; }
}

public record BaseRoomDto
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }
    public bool Availability { get; set; }
    public int MembershipTypeId { get; set; }
}