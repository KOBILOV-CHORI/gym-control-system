using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Trainer;

public interface ITrainerService
{
    Result<PaginationResponse<IEnumerable<TrainerReadDto>>> GetAllTrainers(TrainerFilter filter);
    Result<TrainerReadDto> GetTrainerById(int id);
    Result<bool> CreateTrainer(TrainerCreateDto createDto);
    Result<bool> UpdateTrainer(TrainerUpdateDto updateDto);
    Result<bool> DeleteTrainer(int id);
}