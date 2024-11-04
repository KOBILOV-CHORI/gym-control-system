using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MembershipType;

public class MembershipTypeService : IMembershipTypeService
{
    private readonly DataContext context;

    public MembershipTypeService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<MembershipTypeReadDto>>> GetAllMembershipTypes(MembershipTypeFilter filter)
    {
        var membershipTypes = context.MembershipTypes.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            membershipTypes = membershipTypes.Where(mt => mt.Name.Contains(filter.Name));

        int totalRecords = membershipTypes.Count();
        var result = membershipTypes.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(mt => !mt.IsDeleted)
            .Select(mt => mt.MembershipTypeToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<MembershipTypeReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<MembershipTypeReadDto>>>.Success(paginationResponse);
    }

    public Result<MembershipTypeReadDto> GetMembershipTypeById(int id)
    {
        var membershipType = context.MembershipTypes
            .Where(mt => !mt.IsDeleted && mt.Id == id)
            .Select(mt => mt.MembershipTypeToReadDto())
            .FirstOrDefault();

        return membershipType == null
            ? Result<MembershipTypeReadDto>.Failure(Error.NotFound("Membership Type with the specified ID was not found."))
            : Result<MembershipTypeReadDto>.Success(membershipType);
    }

    public Result<bool> CreateMembershipType(MembershipTypeCreateDto createDto)
    {
        var newMembershipType = createDto.CreateDtoToMembershipType();
        context.MembershipTypes.Add(newMembershipType);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateMembershipType(MembershipTypeUpdateDto updateDto)
    {
        var existingMembershipType = context.MembershipTypes.FirstOrDefault(mt => !mt.IsDeleted && mt.Id == updateDto.Id);
        if (existingMembershipType == null)
            return Result<bool>.Failure(Error.NotFound("Membership Type not found for update."));

        existingMembershipType.UpdateDtoToMembershipType(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteMembershipType(int id)
    {
        var existingMembershipType = context.MembershipTypes.FirstOrDefault(mt => mt.Id == id && !mt.IsDeleted);
        if (existingMembershipType == null)
            return Result<bool>.Failure(Error.NotFound("Membership Type not found for deletion."));

        existingMembershipType.IsDeleted = true;
        existingMembershipType.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
