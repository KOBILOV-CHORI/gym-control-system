using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class MembershipMapperExtension
{
    public static MembershipReadDto MembershipToReadDto(this Membership membershipEntity)
    {
        return new MembershipReadDto
        {
            Id = membershipEntity.Id,
            MembershipTypeId = membershipEntity.MembershipTypeId,
            ClientId = membershipEntity.ClientId,
            StartDate = membershipEntity.StartDate,
            EndDate = membershipEntity.EndDate
        };
    }

    public static Membership CreateDtoToMembership(this MembershipCreateDto createDto)
    {
        return new Membership
        {
            MembershipTypeId = createDto.MembershipTypeId,
            ClientId = createDto.ClientId,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Membership UpdateDtoToMembership(this Membership membershipEntity, MembershipUpdateDto updateDto)
    {
        membershipEntity.MembershipTypeId = updateDto.MembershipTypeId;
        membershipEntity.StartDate = updateDto.StartDate;
        membershipEntity.EndDate = updateDto.EndDate;
        membershipEntity.UpdatedAt = DateTime.UtcNow;
        membershipEntity.Version += 1;
        return membershipEntity;
    }

    public static Membership DeleteDtoToMembership(this Membership membershipEntity)
    {
        membershipEntity.IsDeleted = true;
        membershipEntity.DeletedAt = DateTime.UtcNow;
        membershipEntity.UpdatedAt = DateTime.UtcNow;
        membershipEntity.Version += 1;
        return membershipEntity;
    }
}