using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class AdministratorMapperExtension
{
    public static AdministratorReadDto AdministratorToReadDto(this Administrator adminEntity)
    {
        return new AdministratorReadDto
        {
            Id = adminEntity.Id,
            Username = adminEntity.Username,
            RoleId = adminEntity.RoleId,
            Email = adminEntity.Email,
            PhoneNumber = adminEntity.PhoneNumber
        };
    }

    public static Administrator CreateDtoToAdministrator(this AdministratorCreateDto createDto)
    {
        return new Administrator
        {
            Username = createDto.Username,
            PasswordHash = createDto.PasswordHash,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Administrator UpdateDtoToAdministrator(this Administrator adminEntity, AdministratorUpdateDto updateDto)
    {
        adminEntity.Username = updateDto.Username;
        adminEntity.PasswordHash = updateDto.PasswordHash;
        adminEntity.Email = updateDto.Email;
        adminEntity.PhoneNumber = updateDto.PhoneNumber;
        adminEntity.UpdatedAt = DateTime.UtcNow;
        adminEntity.Version += 1;
        return adminEntity;
    }

    public static Administrator DeleteDtoToAdministrator(this Administrator adminEntity)
    {
        adminEntity.IsDeleted = true;
        adminEntity.DeletedAt = DateTime.UtcNow;
        adminEntity.UpdatedAt = DateTime.UtcNow;
        adminEntity.Version += 1;
        return adminEntity;
    }
}