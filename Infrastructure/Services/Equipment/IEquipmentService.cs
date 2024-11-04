using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Equipment;

public interface IEquipmentService
{
    Result<PaginationResponse<IEnumerable<EquipmentReadDto>>> GetAllEquipments(EquipmentFilter filter);
    Result<EquipmentReadDto> GetEquipmentById(int id);
    Result<bool> CreateEquipment(EquipmentCreateDto createDto);
    Result<bool> UpdateEquipment(EquipmentUpdateDto updateDto);
    Result<bool> DeleteEquipment(int id);
}