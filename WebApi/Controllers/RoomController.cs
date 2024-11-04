using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Room;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/rooms")]
public sealed class RoomController(IRoomService roomService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetRooms([FromQuery] RoomFilter filter)
        => (roomService.GetAllRooms(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetRoomById(int id)
        => (roomService.GetRoomById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateRoom([FromBody] RoomCreateDto dto)
        => (roomService.CreateRoom(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateRoom(int id,[FromBody] RoomUpdateDto dto)
        => (roomService.UpdateRoom(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoom(int id)
        => (roomService.DeleteRoom(id)).ToActionResult();
}