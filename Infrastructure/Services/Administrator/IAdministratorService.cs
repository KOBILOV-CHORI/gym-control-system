using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Administrator;

public interface IAdministratorService
{
    Result<PaginationResponse<IEnumerable<AdministratorReadDto>>> GetAllAdministrators(AdministratorFilter filter);
    Result<AdministratorReadDto> GetAdministratorById(int id);
    Result<bool> CreateAdministrator(AdministratorCreateDto createDto);
    Result<bool> UpdateAdministrator(AdministratorUpdateDto updateDto);
    Result<bool> DeleteAdministrator(int id);
}