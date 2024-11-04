using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class MembershipTypeMapperExtension
{
    public static MembershipTypeReadDto MembershipTypeToReadDto(this MembershipType membershipTypeEntity)
    {
        return new MembershipTypeReadDto
        {
            Id = membershipTypeEntity.Id,
            Name = membershipTypeEntity.Name,
            Description = membershipTypeEntity.Description,
            Price = membershipTypeEntity.Price,
            DurationInDays = membershipTypeEntity.DurationInDays,
            CategoryId = membershipTypeEntity.CategoryId
        };
    }

    public static MembershipType CreateDtoToMembershipType(this MembershipTypeCreateDto createDto)
    {
        return new MembershipType
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Price = createDto.Price,
            DurationInDays = createDto.DurationInDays,
            CategoryId = createDto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static MembershipType UpdateDtoToMembershipType(this MembershipType membershipTypeEntity, MembershipTypeUpdateDto updateDto)
    {
        membershipTypeEntity.Name = updateDto.Name;
        membershipTypeEntity.Description = updateDto.Description;
        membershipTypeEntity.Price = updateDto.Price;
        membershipTypeEntity.DurationInDays = updateDto.DurationInDays;
        membershipTypeEntity.CategoryId = updateDto.CategoryId;
        membershipTypeEntity.UpdatedAt = DateTime.UtcNow;
        membershipTypeEntity.Version += 1;
        return membershipTypeEntity;
    }

    public static MembershipType DeleteDtoToMembershipType(this MembershipType membershipTypeEntity)
    {
        membershipTypeEntity.IsDeleted = true;
        membershipTypeEntity.DeletedAt = DateTime.UtcNow;
        membershipTypeEntity.UpdatedAt = DateTime.UtcNow;
        membershipTypeEntity.Version += 1;
        return membershipTypeEntity;
    }
}