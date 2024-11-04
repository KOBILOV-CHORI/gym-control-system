using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Class;

public class ClassService : IClassService
{
    private readonly DataContext context;

    public ClassService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<ClassReadDto>>> GetAllClasses(ClassFilter filter)
    {
        var classes = context.Classes.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            classes = classes.Where(c => c.Name.Contains(filter.Name));
        if (filter.TrainerId != null)
            classes = classes.Where(c => c.TrainerId == filter.TrainerId);
        if (filter.RoomId != null)
            classes = classes.Where(c => c.RoomId == filter.RoomId);

        int totalRecords = classes.Count();
        var result = classes.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(c => !c.IsDeleted)
            .Select(c => c.ClassToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<ClassReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<ClassReadDto>>>.Success(paginationResponse);
    }

    public Result<ClassReadDto> GetClassById(int id)
    {
        var classEntity = context.Classes
            .Where(c => !c.IsDeleted && c.Id == id)
            .Select(c => c.ClassToReadDto())
            .FirstOrDefault();

        return classEntity == null
            ? Result<ClassReadDto>.Failure(Error.NotFound("Class with the specified ID was not found."))
            : Result<ClassReadDto>.Success(classEntity);
    }

    public Result<bool> CreateClass(ClassCreateDto createDto)
    {
        var newClass = createDto.CreateDtoToClass();
        context.Classes.Add(newClass);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateClass(ClassUpdateDto updateDto)
    {
        var existingClass = context.Classes.FirstOrDefault(c => !c.IsDeleted && c.Id == updateDto.Id);
        if (existingClass == null)
            return Result<bool>.Failure(Error.NotFound("Class not found for update."));

        existingClass.UpdateDtoToClass(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteClass(int id)
    {
        var existingClass = context.Classes.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
        if (existingClass == null)
            return Result<bool>.Failure(Error.NotFound("Class not found for deletion."));

        existingClass.IsDeleted = true;
        existingClass.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
