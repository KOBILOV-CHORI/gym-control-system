using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Service;

public class ServiceService : IServiceService
{
    private readonly DataContext context;

    public ServiceService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<ServiceReadDto>>> GetAllServices(ServiceFilter filter)
    {
        var services = context.Services.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            services = services.Where(s => s.Name.Contains(filter.Name));

        int totalRecords = services.Count();
        var result = services.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(s => !s.IsDeleted)
            .Select(s => s.ServiceToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<ServiceReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<ServiceReadDto>>>.Success(paginationResponse);
    }

    public Result<ServiceReadDto> GetServiceById(int id)
    {
        var service = context.Services
            .Where(s => !s.IsDeleted && s.Id == id)
            .Select(s => s.ServiceToReadDto())
            .FirstOrDefault();

        return service == null
            ? Result<ServiceReadDto>.Failure(Error.NotFound("Service with the specified ID was not found."))
            : Result<ServiceReadDto>.Success(service);
    }

    public Result<bool> CreateService(ServiceCreateDto createDto)
    {
        var newService = createDto.CreateDtoToService();
        context.Services.Add(newService);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateService(ServiceUpdateDto updateDto)
    {
        var existingService = context.Services.FirstOrDefault(s => !s.IsDeleted && s.Id == updateDto.Id);
        if (existingService == null)
            return Result<bool>.Failure(Error.NotFound("Service not found for update."));

        existingService.UpdateDtoToService(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteService(int id)
    {
        var existingService = context.Services.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
        if (existingService == null)
            return Result<bool>.Failure(Error.NotFound("Service not found for deletion."));

        existingService.IsDeleted = true;
        existingService.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
