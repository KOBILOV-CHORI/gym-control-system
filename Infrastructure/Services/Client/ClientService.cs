using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Client;

public class ClientService : IClientService
{
    private readonly DataContext context;

    public ClientService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<ClientReadDto>>> GetAllClients(ClientFilter filter)
    {
        var clients = context.Clients.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FirstName))
            clients = clients.Where(c => c.FirstName.Contains(filter.FirstName));
        if (!string.IsNullOrEmpty(filter.LastName))
            clients = clients.Where(c => c.LastName.Contains(filter.LastName));
        if (!string.IsNullOrEmpty(filter.Email))
            clients = clients.Where(c => c.Email.Contains(filter.Email));
        if (!string.IsNullOrEmpty(filter.PhoneNumber))
            clients = clients.Where(c => c.PhoneNumber.Contains(filter.PhoneNumber));
        if (filter.IsActive != null)
            clients = clients.Where(c => c.IsActive == filter.IsActive);

        int totalRecords = clients.Count();
        var result = clients.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(c => !c.IsDeleted)
            .Select(c => c.ClientToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<ClientReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<ClientReadDto>>>.Success(paginationResponse);
    }

    public Result<ClientReadDto> GetClientById(int id)
    {
        var client = context.Clients
            .Where(c => !c.IsDeleted && c.Id == id)
            .Select(c => c.ClientToReadDto())
            .FirstOrDefault();

        return client == null
            ? Result<ClientReadDto>.Failure(Error.NotFound("Client with the specified ID was not found."))
            : Result<ClientReadDto>.Success(client);
    }

    public Result<bool> CreateClient(ClientCreateDto createDto)
    {
        var newClient = createDto.CreateDtoToClient();
        context.Clients.Add(newClient);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateClient(ClientUpdateDto updateDto)
    {
        var existingClient = context.Clients.FirstOrDefault(c => !c.IsDeleted && c.Id == updateDto.Id);
        if (existingClient == null)
            return Result<bool>.Failure(Error.NotFound("Client not found for update."));

        existingClient.UpdateDtoToClient(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteClient(int id)
    {
        var existingClient = context.Clients.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
        if (existingClient == null)
            return Result<bool>.Failure(Error.NotFound("Client not found for deletion."));

        existingClient.IsDeleted = true;
        existingClient.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
