using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Trainer;

public class TrainerService : ITrainerService
{
    private readonly DataContext context;

    public TrainerService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<TrainerReadDto>>> GetAllTrainers(TrainerFilter filter)
    {
        var trainers = context.Trainers.AsQueryable();

        if (!string.IsNullOrEmpty(filter.FirstName))
            trainers = trainers.Where(t => t.FirstName.Contains(filter.FirstName));
        if (!string.IsNullOrEmpty(filter.LastName))
            trainers = trainers.Where(t => t.LastName.Contains(filter.LastName));
        if (!string.IsNullOrEmpty(filter.Specialization))
            trainers = trainers.Where(t => t.Specialization.Contains(filter.Specialization));

        int totalRecords = trainers.Count();
        var result = trainers.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(t => !t.IsDeleted)
            .Select(t => t.TrainerToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<TrainerReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<TrainerReadDto>>>.Success(paginationResponse);
    }

    public Result<TrainerReadDto> GetTrainerById(int id)
    {
        var trainer = context.Trainers
            .Where(t => !t.IsDeleted && t.Id == id)
            .Select(t => t.TrainerToReadDto())
            .FirstOrDefault();

        return trainer == null
            ? Result<TrainerReadDto>.Failure(Error.NotFound("Trainer with the specified ID was not found."))
            : Result<TrainerReadDto>.Success(trainer);
    }

    public Result<bool> CreateTrainer(TrainerCreateDto createDto)
    {
        var newTrainer = createDto.CreateDtoToTrainer();
        context.Trainers.Add(newTrainer);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateTrainer(TrainerUpdateDto updateDto)
    {
        var existingTrainer = context.Trainers.FirstOrDefault(t => !t.IsDeleted && t.Id == updateDto.Id);
        if (existingTrainer == null)
            return Result<bool>.Failure(Error.NotFound("Trainer not found for update."));

        existingTrainer.UpdateDtoToTrainer(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteTrainer(int id)
    {
        var existingTrainer = context.Trainers.FirstOrDefault(t => t.Id == id && !t.IsDeleted);
        if (existingTrainer == null)
            return Result<bool>.Failure(Error.NotFound("Trainer not found for deletion."));

        existingTrainer.IsDeleted = true;
        existingTrainer.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
