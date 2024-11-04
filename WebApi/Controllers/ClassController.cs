using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Class;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/classs")]
public sealed class ClassController(IClassService classService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetClasses([FromQuery] ClassFilter filter)
        => (classService.GetAllClasses(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetClassById(int id)
        => (classService.GetClassById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateClass([FromBody] ClassCreateDto dto)
        => (classService.CreateClass(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateClass(int id,[FromBody] ClassUpdateDto dto)
        => (classService.UpdateClass(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteClass(int id)
        => (classService.DeleteClass(id)).ToActionResult();
}