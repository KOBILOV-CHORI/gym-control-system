namespace Domain.Dtos;

public record EquipmentUpdateDto : BaseEquipmentDto
{
    public int Id { get; set; }
}

public record EquipmentCreateDto : BaseEquipmentDto {}

public record EquipmentReadDto : BaseEquipmentDto
{
    public int Id { get; set; }
}

public record BaseEquipmentDto
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public int RoomId { get; set; }
}