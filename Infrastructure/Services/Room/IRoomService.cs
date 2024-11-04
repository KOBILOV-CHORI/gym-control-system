using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Room;

public interface IRoomService
{
    Result<PaginationResponse<IEnumerable<RoomReadDto>>> GetAllRooms(RoomFilter filter);
    Result<RoomReadDto> GetRoomById(int id);
    Result<bool> CreateRoom(RoomCreateDto createDto);
    Result<bool> UpdateRoom(RoomUpdateDto updateDto);
    Result<bool> DeleteRoom(int id);
}