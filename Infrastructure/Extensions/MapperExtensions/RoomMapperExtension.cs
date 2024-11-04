using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class RoomMapperExtension
{
    public static RoomReadDto RoomToReadDto(this Room roomEntity)
    {
        return new RoomReadDto
        {
            Id = roomEntity.Id,
            Name = roomEntity.Name,
            Capacity = roomEntity.Capacity,
            Location = roomEntity.Location,
            Availability = roomEntity.Availability
        };
    }

    public static Room CreateDtoToRoom(this RoomCreateDto createDto)
    {
        return new Room
        {
            Name = createDto.Name,
            Capacity = createDto.Capacity,
            Location = createDto.Location,
            Availability = createDto.Availability,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Room UpdateDtoToRoom(this Room roomEntity, RoomUpdateDto updateDto)
    {
        roomEntity.Name = updateDto.Name;
        roomEntity.Capacity = updateDto.Capacity;
        roomEntity.Location = updateDto.Location;
        roomEntity.Availability = updateDto.Availability;
        roomEntity.UpdatedAt = DateTime.UtcNow;
        roomEntity.Version += 1;
        return roomEntity;
    }

    public static Room DeleteDtoToRoom(this Room roomEntity)
    {
        roomEntity.IsDeleted = true;
        roomEntity.DeletedAt = DateTime.UtcNow;
        roomEntity.UpdatedAt = DateTime.UtcNow;
        roomEntity.Version += 1;
        return roomEntity;
    }
}