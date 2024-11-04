using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class ClientMapperExtension
{
    public static ClientReadDto ClientToReadDto(this Client clientEntity)
    {
        return new ClientReadDto
        {
            Id = clientEntity.Id,
            FirstName = clientEntity.FirstName,
            LastName = clientEntity.LastName,
            Email = clientEntity.Email,
            PhoneNumber = clientEntity.PhoneNumber,
            DateOfBirth = clientEntity.DateOfBirth,
            IsActive = clientEntity.IsActive
        };
    }

    public static Client CreateDtoToClient(this ClientCreateDto createDto)
    {
        return new Client
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber,
            DateOfBirth = createDto.DateOfBirth,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    public static Client UpdateDtoToClient(this Client clientEntity, ClientUpdateDto updateDto)
    {
        clientEntity.FirstName = updateDto.FirstName;
        clientEntity.LastName = updateDto.LastName;
        clientEntity.Email = updateDto.Email;
        clientEntity.PhoneNumber = updateDto.PhoneNumber;
        clientEntity.DateOfBirth = updateDto.DateOfBirth;
        clientEntity.UpdatedAt = DateTime.UtcNow;
        clientEntity.Version += 1;
        return clientEntity;
    }

    public static Client DeleteDtoToClient(this Client clientEntity)
    {
        clientEntity.IsDeleted = true;
        clientEntity.DeletedAt = DateTime.UtcNow;
        clientEntity.UpdatedAt = DateTime.UtcNow;
        clientEntity.Version += 1;
        return clientEntity;
    }
}