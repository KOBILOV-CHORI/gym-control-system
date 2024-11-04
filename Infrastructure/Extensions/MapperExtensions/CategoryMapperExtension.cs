using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class CategoryMapperExtension
{
    public static CategoryReadDto CategoryToReadDto(this Category categoryEntity)
    {
        return new CategoryReadDto
        {
            Id = categoryEntity.Id,
            Name = categoryEntity.Name,
            Description = categoryEntity.Description
        };
    }

    public static Category CreateDtoToCategory(this CategoryCreateDto createDto)
    {
        return new Category
        {
            Name = createDto.Name,
            Description = createDto.Description,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Category UpdateDtoToCategory(this Category categoryEntity, CategoryUpdateDto updateDto)
    {
        categoryEntity.Name = updateDto.Name;
        categoryEntity.Description = updateDto.Description;
        categoryEntity.UpdatedAt = DateTime.UtcNow;
        categoryEntity.Version += 1;
        return categoryEntity;
    }

    public static Category DeleteDtoToCategory(this Category categoryEntity)
    {
        categoryEntity.IsDeleted = true;
        categoryEntity.DeletedAt = DateTime.UtcNow;
        categoryEntity.UpdatedAt = DateTime.UtcNow;
        categoryEntity.Version += 1;
        return categoryEntity;
    }
}