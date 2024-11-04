namespace Domain.Dtos;

public record ClassUpdateDto : BaseClassDto
{
    public int Id { get; set; }
}

public record ClassCreateDto : BaseClassDto {}

public record ClassReadDto : BaseClassDto
{
    public int Id { get; set; }
}

public record BaseClassDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
    public string Level { get; set; }
    public int FrequencyPerWeek { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TrainerId { get; set; }
    public int RoomId { get; set; }
    public int CategoryId { get; set; }
}