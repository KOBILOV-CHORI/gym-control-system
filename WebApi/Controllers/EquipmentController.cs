using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Equipment;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/equipments")]
public sealed class EquipmentController(EquipmentService equipmentService) : ControllerBase
{
    [HttpGet]
    public IActionResult Equipments([FromQuery] EquipmentFilter filter)
        => (equipmentService.GetAllEquipments(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult EquipmentById(int id)
        => (equipmentService.GetEquipmentById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult Equipment([FromBody] EquipmentCreateDto dto)
        => (equipmentService.CreateEquipment(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult Equipment(int id,[FromBody] EquipmentUpdateDto dto)
        => (equipmentService.UpdateEquipment(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult Equipment(int id)
        => (equipmentService.DeleteEquipment(id)).ToActionResult();
}