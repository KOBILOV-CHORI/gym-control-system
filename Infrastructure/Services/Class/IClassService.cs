using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Class;

public interface IClassService
{
    Result<PaginationResponse<IEnumerable<ClassReadDto>>> GetAllClasses(ClassFilter filter);
    Result<ClassReadDto> GetClassById(int id);
    Result<bool> CreateClass(ClassCreateDto createDto);
    Result<bool> UpdateClass(ClassUpdateDto updateDto);
    Result<bool> DeleteClass(int id);
}