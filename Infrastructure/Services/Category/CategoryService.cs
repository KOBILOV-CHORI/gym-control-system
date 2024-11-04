using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly DataContext context;

    public CategoryService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<CategoryReadDto>>> GetAllCategories(CategoryFilter filter)
    {
        var categories = context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            categories = categories.Where(c => c.Name.Contains(filter.Name));

        int totalRecords = categories.Count();
        var result = categories.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(c => !c.IsDeleted)
            .Select(c => c.CategoryToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<CategoryReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<CategoryReadDto>>>.Success(paginationResponse);
    }

    public Result<CategoryReadDto> GetCategoryById(int id)
    {
        var category = context.Categories
            .Where(c => !c.IsDeleted && c.Id == id)
            .Select(c => c.CategoryToReadDto())
            .FirstOrDefault();

        return category == null
            ? Result<CategoryReadDto>.Failure(Error.NotFound("Category with the specified ID was not found."))
            : Result<CategoryReadDto>.Success(category);
    }

    public Result<bool> CreateCategory(CategoryCreateDto createDto)
    {
        var newCategory = createDto.CreateDtoToCategory();
        context.Categories.Add(newCategory);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateCategory(CategoryUpdateDto updateDto)
    {
        var category = context.Categories.FirstOrDefault(c => !c.IsDeleted && c.Id == updateDto.Id);
        if (category == null)
            return Result<bool>.Failure(Error.NotFound("Category not found for update."));

        category.UpdateDtoToCategory(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteCategory(int id)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
        if (category == null)
            return Result<bool>.Failure(Error.NotFound("Category not found for deletion."));

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
