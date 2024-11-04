using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.Extensions.MapperExtensions;

public static class TrainerMapperExtension
{
    public static TrainerReadDto TrainerToReadDto(this Trainer trainerEntity)
    {
        return new TrainerReadDto
        {
            Id = trainerEntity.Id,
            FirstName = trainerEntity.FirstName,
            LastName = trainerEntity.LastName,
            Specialization = trainerEntity.Specialization,
            ExperienceYears = trainerEntity.ExperienceYears,
            Rating = trainerEntity.Rating,
        };
    }

    public static Trainer CreateDtoToTrainer(this TrainerCreateDto createDto)
    {
        return new Trainer
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Specialization = createDto.Specialization,
            ExperienceYears = createDto.ExperienceYears,
            Rating = createDto.Rating,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Trainer UpdateDtoToTrainer(this Trainer trainerEntity, TrainerUpdateDto updateDto)
    {
        trainerEntity.FirstName = updateDto.FirstName;
        trainerEntity.LastName = updateDto.LastName;
        trainerEntity.Specialization = updateDto.Specialization;
        trainerEntity.ExperienceYears = updateDto.ExperienceYears;
        trainerEntity.Rating = updateDto.Rating;
        trainerEntity.UpdatedAt = DateTime.UtcNow;
        trainerEntity.Version += 1;
        return trainerEntity;
    }

    public static Trainer DeleteDtoToTrainer(this Trainer trainerEntity)
    {
        trainerEntity.IsDeleted = true;
        trainerEntity.DeletedAt = DateTime.UtcNow;
        trainerEntity.UpdatedAt = DateTime.UtcNow;
        trainerEntity.Version += 1;
        return trainerEntity;
    }
}