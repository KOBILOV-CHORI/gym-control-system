using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Administrator;

public class AdministratorService : IAdministratorService
{
    private readonly DataContext context;

    public AdministratorService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<AdministratorReadDto>>> GetAllAdministrators(AdministratorFilter filter)
    {
        var administrators = context.Administrators.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Username))
            administrators = administrators.Where(a => a.Username.Contains(filter.Username));
        if (!string.IsNullOrEmpty(filter.Email))
            administrators = administrators.Where(a => a.Email.Contains(filter.Email));

        int totalRecords = administrators.Count();
        var result = administrators.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(a => !a.IsDeleted)
            .Select(a => a.AdministratorToReadDto())
            .ToList();
        var paginationResponse = PaginationResponse<IEnumerable<AdministratorReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<AdministratorReadDto>>>.Success(paginationResponse);
    }

    public Result<AdministratorReadDto> GetAdministratorById(int id)
    {
        var car = context.Administrators
            .Where(x => !x.IsDeleted && x.Id == id)
            .Select(x => x.AdministratorToReadDto())
            .FirstOrDefault();

        return car == null
            ? Result<AdministratorReadDto>.Failure(Error.NotFound("Administrator with the specified ID was not found."))
            : Result<AdministratorReadDto>.Success(car);
    }

    public Result<bool> CreateAdministrator(AdministratorCreateDto createDto)
    {
        bool conflict = context.Administrators.Any(x => x.Username.ToLower() == createDto.Username.ToLower() && !x.IsDeleted);
        if (conflict)
            return Result<bool>.Failure(Error.AlreadyExist());

        context.Administrators.AddAsync(createDto.CreateDtoToAdministrator());
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateAdministrator(AdministratorUpdateDto updateDto)
    {
        var existingCar = context.Administrators.FirstOrDefault(x => !x.IsDeleted && x.Id == updateDto.Id);
        if (existingCar == null)
            return Result<bool>.Failure(Error.NotFound("Car not found for the update."));

        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteAdministrator(int id)
    {
        var existingAdministrator = context.Administrators.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        if (existingAdministrator == null)
            return Result<bool>.Failure(Error.NotFound("Administrator not found for deletion."));

        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
