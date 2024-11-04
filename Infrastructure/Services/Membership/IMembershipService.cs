using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Membership;

public interface IMembershipService
{
    Result<PaginationResponse<IEnumerable<MembershipReadDto>>> GetAllMemberships(MembershipFilter filter);
    Result<MembershipReadDto> GetMembershipById(int id);
    Result<bool> CreateMembership(MembershipCreateDto createDto);
    Result<bool> UpdateMembership(MembershipUpdateDto updateDto);
    Result<bool> DeleteMembership(int id);
}