using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Category;

public interface ICategoryService
{
    Result<PaginationResponse<IEnumerable<CategoryReadDto>>> GetAllCategories(CategoryFilter filter);
    Result<CategoryReadDto> GetCategoryById(int id);
    Result<bool> CreateCategory(CategoryCreateDto createDto);
    Result<bool> UpdateCategory(CategoryUpdateDto updateDto);
    Result<bool> DeleteCategory(int id);
}