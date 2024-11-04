namespace Domain.Dtos;

public record BookingUpdateDto : BaseBookingDto
{
    public int Id { get; set; }
}

public record BookingCreateDto : BaseBookingDto {}

public record BookingReadDto : BaseBookingDto
{
    public int Id { get; set; }
}

public record BaseBookingDto
{
    public int ClientId { get; set; }
    public int ClassId { get; set; }
}