using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Client;

public interface IClientService
{
    Result<PaginationResponse<IEnumerable<ClientReadDto>>> GetAllClients(ClientFilter filter);
    Result<ClientReadDto> GetClientById(int id);
    Result<bool> CreateClient(ClientCreateDto createDto);
    Result<bool> UpdateClient(ClientUpdateDto updateDto);
    Result<bool> DeleteClient(int id);
}