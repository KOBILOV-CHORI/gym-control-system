using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class ClassMapperExtension
{
    public static ClassReadDto ClassToReadDto(this Class classEntity)
    {
        return new ClassReadDto
        {
            Id = classEntity.Id,
            Name = classEntity.Name,
            Description = classEntity.Description,
            Duration = classEntity.Duration,
            Capacity = classEntity.Capacity,
            Level = classEntity.Level,
            TrainerId = classEntity.TrainerId,
            RoomId = classEntity.RoomId,
            CategoryId = classEntity.CategoryId
        };
    }

    public static Class CreateDtoToClass(this ClassCreateDto createDto)
    {
        return new Class
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Duration = createDto.Duration,
            Capacity = createDto.Capacity,
            Level = createDto.Level,
            TrainerId = createDto.TrainerId,
            RoomId = createDto.RoomId,
            CategoryId = createDto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Class UpdateDtoToClass(this Class classEntity, ClassUpdateDto updateDto)
    {
        classEntity.Name = updateDto.Name;
        classEntity.Description = updateDto.Description;
        classEntity.Duration = updateDto.Duration;
        classEntity.Capacity = updateDto.Capacity;
        classEntity.Level = updateDto.Level;
        classEntity.TrainerId = updateDto.TrainerId;
        classEntity.RoomId = updateDto.RoomId;
        classEntity.CategoryId = updateDto.CategoryId;
        classEntity.UpdatedAt = DateTime.UtcNow;
        classEntity.Version += 1;
        return classEntity;
    }

    public static Class DeleteDtoToClass(this Class classEntity)
    {
        classEntity.IsDeleted = true;
        classEntity.DeletedAt = DateTime.UtcNow;
        classEntity.UpdatedAt = DateTime.UtcNow;
        classEntity.Version += 1;
        return classEntity;
    }
}