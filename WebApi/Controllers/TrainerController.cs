using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Trainer;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/trainers")]
public sealed class TrainerController(ITrainerService trainerService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetTrainers([FromQuery] TrainerFilter filter)
        => (trainerService.GetAllTrainers(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetTrainerById(int id)
        => (trainerService.GetTrainerById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateTrainer([FromBody] TrainerCreateDto dto)
        => (trainerService.CreateTrainer(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateTrainer(int id,[FromBody] TrainerUpdateDto dto)
        => (trainerService.UpdateTrainer(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteTrainer(int id)
        => (trainerService.DeleteTrainer(id)).ToActionResult();
}