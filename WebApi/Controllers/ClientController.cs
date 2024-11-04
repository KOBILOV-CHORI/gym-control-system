using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Client;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/clients")]
public sealed class ClientController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetClients([FromQuery] ClientFilter filter)
        => (clientService.GetAllClients(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetClientById(int id)
        => (clientService.GetClientById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateClient([FromBody] ClientCreateDto dto)
        => (clientService.CreateClient(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateClient(int id,[FromBody] ClientUpdateDto dto)
        => (clientService.UpdateClient(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteClient(int id)
        => (clientService.DeleteClient(id)).ToActionResult();
}