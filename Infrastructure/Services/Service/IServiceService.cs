using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Service;

public interface IServiceService
{
    Result<PaginationResponse<IEnumerable<ServiceReadDto>>> GetAllServices(ServiceFilter filter);
    Result<ServiceReadDto> GetServiceById(int id);
    Result<bool> CreateService(ServiceCreateDto createDto);
    Result<bool> UpdateService(ServiceUpdateDto updateDto);
    Result<bool> DeleteService(int id);
}