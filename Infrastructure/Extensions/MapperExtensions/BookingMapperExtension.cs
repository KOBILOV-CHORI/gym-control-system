using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class BookingMapperExtension
{
    public static BookingReadDto BookingToReadDto(this Booking bookingEntity)
    {
        return new BookingReadDto
        {
            Id = bookingEntity.Id,
            ClientId = bookingEntity.ClientId,
            ClassId = bookingEntity.ClassId
        };
    }

    public static Booking CreateDtoToBooking(this BookingCreateDto createDto)
    {
        return new Booking
        {
            ClientId = createDto.ClientId,
            ClassId = createDto.ClassId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Booking UpdateDtoToBooking(this Booking bookingEntity, BookingUpdateDto updateDto)
    {
        bookingEntity.ClientId = updateDto.ClientId;
        bookingEntity.ClassId = updateDto.ClassId;
        bookingEntity.UpdatedAt = DateTime.UtcNow;
        bookingEntity.Version += 1;
        return bookingEntity;
    }

    public static Booking DeleteDtoToBooking(this Booking bookingEntity)
    {
        bookingEntity.IsDeleted = true;
        bookingEntity.DeletedAt = DateTime.UtcNow;
        bookingEntity.UpdatedAt = DateTime.UtcNow;
        bookingEntity.Version += 1;
        return bookingEntity;
    }
}