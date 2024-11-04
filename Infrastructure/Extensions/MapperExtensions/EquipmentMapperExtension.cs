using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class EquipmentMapperExtension
{
    public static EquipmentReadDto EquipmentToReadDto(this Equipment equipmentEntity)
    {
        return new EquipmentReadDto
        {
            Id = equipmentEntity.Id,
            Name = equipmentEntity.Name,
            Type = equipmentEntity.Type,
            Quantity = equipmentEntity.Quantity,
            RoomId = equipmentEntity.RoomId
        };
    }

    public static Equipment CreateDtoToEquipment(this EquipmentCreateDto createDto)
    {
        return new Equipment
        {
            Name = createDto.Name,
            Type = createDto.Type,
            Quantity = createDto.Quantity,
            RoomId = createDto.RoomId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Equipment UpdateDtoToEquipment(this Equipment equipmentEntity, EquipmentUpdateDto updateDto)
    {
        equipmentEntity.Name = updateDto.Name;
        equipmentEntity.Type = updateDto.Type;
        equipmentEntity.Quantity = updateDto.Quantity;
        equipmentEntity.RoomId = updateDto.RoomId;
        equipmentEntity.UpdatedAt = DateTime.UtcNow;
        equipmentEntity.Version += 1;
        return equipmentEntity;
    }

    public static Equipment DeleteDtoToEquipment(this Equipment equipmentEntity)
    {
        equipmentEntity.IsDeleted = true;
        equipmentEntity.DeletedAt = DateTime.UtcNow;
        equipmentEntity.UpdatedAt = DateTime.UtcNow;
        equipmentEntity.Version += 1;
        return equipmentEntity;
    }
}