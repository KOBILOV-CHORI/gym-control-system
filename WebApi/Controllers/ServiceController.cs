using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/services")]
public sealed class ServiceController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetServices([FromQuery] ServiceFilter filter)
        => (serviceService.GetAllServices(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetServiceById(int id)
        => (serviceService.GetServiceById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateService([FromBody] ServiceCreateDto dto)
        => (serviceService.CreateService(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateService(int id,[FromBody] ServiceUpdateDto dto)
        => (serviceService.UpdateService(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteService(int id)
        => (serviceService.DeleteService(id)).ToActionResult();
}