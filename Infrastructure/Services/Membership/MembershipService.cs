using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Membership;

public class MembershipService : IMembershipService
{
    private readonly DataContext context;

    public MembershipService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<MembershipReadDto>>> GetAllMemberships(MembershipFilter filter)
    {
        var memberships = context.Memberships.AsQueryable();

        if (filter.ClientId != null)
            memberships = memberships.Where(m => m.ClientId == filter.ClientId);
        if (filter.MembershipTypeId != null)
            memberships = memberships.Where(m => m.MembershipTypeId == filter.MembershipTypeId);

        int totalRecords = memberships.Count();
        var result = memberships.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(m => !m.IsDeleted)
            .Select(m => m.MembershipToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<MembershipReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<MembershipReadDto>>>.Success(paginationResponse);
    }

    public Result<MembershipReadDto> GetMembershipById(int id)
    {
        var membership = context.Memberships
            .Where(m => !m.IsDeleted && m.Id == id)
            .Select(m => m.MembershipToReadDto())
            .FirstOrDefault();

        return membership == null
            ? Result<MembershipReadDto>.Failure(Error.NotFound("Membership with the specified ID was not found."))
            : Result<MembershipReadDto>.Success(membership);
    }

    public Result<bool> CreateMembership(MembershipCreateDto createDto)
    {
        var newMembership = createDto.CreateDtoToMembership();
        context.Memberships.Add(newMembership);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateMembership(MembershipUpdateDto updateDto)
    {
        var existingMembership = context.Memberships.FirstOrDefault(m => !m.IsDeleted && m.Id == updateDto.Id);
        if (existingMembership == null)
            return Result<bool>.Failure(Error.NotFound("Membership not found for update."));

        existingMembership.UpdateDtoToMembership(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteMembership(int id)
    {
        var existingMembership = context.Memberships.FirstOrDefault(m => m.Id == id && !m.IsDeleted);
        if (existingMembership == null)
            return Result<bool>.Failure(Error.NotFound("Membership not found for deletion."));

        existingMembership.IsDeleted = true;
        existingMembership.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
