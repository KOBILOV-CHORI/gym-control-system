using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.MembershipType;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/membershipTypes")]
public sealed class MembershipTypeController(IMembershipTypeService membershipTypeService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetMembershipTypes([FromQuery] MembershipTypeFilter filter)
        => (membershipTypeService.GetAllMembershipTypes(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetMembershipTypeById(int id)
        => (membershipTypeService.GetMembershipTypeById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateMembershipType([FromBody] MembershipTypeCreateDto dto)
        => (membershipTypeService.CreateMembershipType(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateMembershipType(int id,[FromBody] MembershipTypeUpdateDto dto)
        => (membershipTypeService.UpdateMembershipType(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteMembershipType(int id)
        => (membershipTypeService.DeleteMembershipType(id)).ToActionResult();
}