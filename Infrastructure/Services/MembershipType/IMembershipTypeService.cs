using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.MembershipType;

public interface IMembershipTypeService
{
    Result<PaginationResponse<IEnumerable<MembershipTypeReadDto>>> GetAllMembershipTypes(MembershipTypeFilter filter);
    Result<MembershipTypeReadDto> GetMembershipTypeById(int id);
    Result<bool> CreateMembershipType(MembershipTypeCreateDto createDto);
    Result<bool> UpdateMembershipType(MembershipTypeUpdateDto updateDto);
    Result<bool> DeleteMembershipType(int id);
}