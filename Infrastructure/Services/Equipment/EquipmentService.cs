using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Equipment;

public class EquipmentService : IEquipmentService
{
    private readonly DataContext context;

    public EquipmentService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<EquipmentReadDto>>> GetAllEquipments(EquipmentFilter filter)
    {
        var equipment = context.Equipments.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            equipment = equipment.Where(e => e.Name.Contains(filter.Name));
        if (!string.IsNullOrEmpty(filter.Type))
            equipment = equipment.Where(e => e.Type.Contains(filter.Type));
        if (filter.RoomId != null)
            equipment = equipment.Where(e => e.RoomId == filter.RoomId);

        int totalRecords = equipment.Count();
        var result = equipment.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(e => !e.IsDeleted)
            .Select(e => e.EquipmentToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<EquipmentReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<EquipmentReadDto>>>.Success(paginationResponse);
    }

    public Result<EquipmentReadDto> GetEquipmentById(int id)
    {
        var equipment = context.Equipments
            .Where(e => !e.IsDeleted && e.Id == id)
            .Select(e => e.EquipmentToReadDto())
            .FirstOrDefault();

        return equipment == null
            ? Result<EquipmentReadDto>.Failure(Error.NotFound("Equipment with the specified ID was not found."))
            : Result<EquipmentReadDto>.Success(equipment);
    }

    public Result<bool> CreateEquipment(EquipmentCreateDto createDto)
    {
        var newEquipment = createDto.CreateDtoToEquipment();
        context.Equipments.Add(newEquipment);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateEquipment(EquipmentUpdateDto updateDto)
    {
        var existingEquipment = context.Equipments.FirstOrDefault(e => !e.IsDeleted && e.Id == updateDto.Id);
        if (existingEquipment == null)
            return Result<bool>.Failure(Error.NotFound("Equipment not found for update."));

        existingEquipment.UpdateDtoToEquipment(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteEquipment(int id)
    {
        var existingEquipment = context.Equipments.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
        if (existingEquipment == null)
            return Result<bool>.Failure(Error.NotFound("Equipment not found for deletion."));

        existingEquipment.IsDeleted = true;
        existingEquipment.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
