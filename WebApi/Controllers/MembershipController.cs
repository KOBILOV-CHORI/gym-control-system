using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Membership;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/memberships")]
public sealed class MembershipController(IMembershipService membershipService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetMemberships([FromQuery] MembershipFilter filter)
        => (membershipService.GetAllMemberships(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetMembershipById(int id)
        => (membershipService.GetMembershipById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateMembership([FromBody] MembershipCreateDto dto)
        => (membershipService.CreateMembership(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateMembership(int id,[FromBody] MembershipUpdateDto dto)
        => (membershipService.UpdateMembership(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteMembership(int id)
        => (membershipService.DeleteMembership(id)).ToActionResult();
}