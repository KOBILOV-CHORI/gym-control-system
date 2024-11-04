using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class ServiceMapperExtension
{
    public static ServiceReadDto ServiceToReadDto(this Service serviceEntity)
    {
        return new ServiceReadDto
        {
            Id = serviceEntity.Id,
            Name = serviceEntity.Name,
            Description = serviceEntity.Description,
            Price = serviceEntity.Price,
            Duration = serviceEntity.Duration,
            TrainerId = serviceEntity.TrainerId,
            RoomId = serviceEntity.RoomId,
            CategoryId = serviceEntity.CategoryId
        };
    }

    public static Service CreateDtoToService(this ServiceCreateDto createDto)
    {
        return new Service
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Price = createDto.Price,
            Duration = createDto.Duration,
            TrainerId = createDto.TrainerId,
            RoomId = createDto.RoomId,
            CategoryId = createDto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Service UpdateDtoToService(this Service serviceEntity, ServiceUpdateDto updateDto)
    {
        serviceEntity.Name = updateDto.Name;
        serviceEntity.Description = updateDto.Description;
        serviceEntity.Price = updateDto.Price;
        serviceEntity.Duration = updateDto.Duration;
        serviceEntity.TrainerId = updateDto.TrainerId;
        serviceEntity.RoomId = updateDto.RoomId;
        serviceEntity.CategoryId = updateDto.CategoryId;
        serviceEntity.UpdatedAt = DateTime.UtcNow;
        serviceEntity.Version += 1;
        return serviceEntity;
    }

    public static Service DeleteDtoToService(this Service serviceEntity)
    {
        serviceEntity.IsDeleted = true;
        serviceEntity.DeletedAt = DateTime.UtcNow;
        serviceEntity.UpdatedAt = DateTime.UtcNow;
        serviceEntity.Version += 1;
        return serviceEntity;
    }
}