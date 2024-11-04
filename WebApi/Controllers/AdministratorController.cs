using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Administrator;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/administrators")]
public sealed class AdministratorController(IAdministratorService administratorService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAdministrators([FromQuery] AdministratorFilter filter)
        => (administratorService.GetAllAdministrators(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAdministratorById(int id)
        => (administratorService.GetAdministratorById(id)).ToActionResult();
    
    [HttpPost]
    public async Task<IActionResult> CreateAdministrator([FromBody] AdministratorCreateDto dto)
        => (administratorService.CreateAdministrator(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAdministrator(int id,[FromBody] AdministratorUpdateDto dto)
        => (administratorService.UpdateAdministrator(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAdministrator(int id)
        => (administratorService.DeleteAdministrator(id)).ToActionResult();
}