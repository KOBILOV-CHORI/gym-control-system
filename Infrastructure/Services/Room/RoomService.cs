using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Room;

public class RoomService : IRoomService
{
    private readonly DataContext context;

    public RoomService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<RoomReadDto>>> GetAllRooms(RoomFilter filter)
    {
        var rooms = context.Rooms.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            rooms = rooms.Where(r => r.Name.Contains(filter.Name));
        if (filter.Availability != null)
            rooms = rooms.Where(r => r.Availability == filter.Availability);
        if (!string.IsNullOrEmpty(filter.Location))
            rooms = rooms.Where(r => r.Location.Contains(filter.Location));

        int totalRecords = rooms.Count();
        var result = rooms.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(r => !r.IsDeleted)
            .Select(r => r.RoomToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<RoomReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<RoomReadDto>>>.Success(paginationResponse);
    }

    public Result<RoomReadDto> GetRoomById(int id)
    {
        var room = context.Rooms
            .Where(r => !r.IsDeleted && r.Id == id)
            .Select(r => r.RoomToReadDto())
            .FirstOrDefault();

        return room == null
            ? Result<RoomReadDto>.Failure(Error.NotFound("Room with the specified ID was not found."))
            : Result<RoomReadDto>.Success(room);
    }

    public Result<bool> CreateRoom(RoomCreateDto createDto)
    {
        var newRoom = createDto.CreateDtoToRoom();
        context.Rooms.Add(newRoom);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateRoom(RoomUpdateDto updateDto)
    {
        var existingRoom = context.Rooms.FirstOrDefault(r => !r.IsDeleted && r.Id == updateDto.Id);
        if (existingRoom == null)
            return Result<bool>.Failure(Error.NotFound("Room not found for update."));

        existingRoom.UpdateDtoToRoom(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteRoom(int id)
    {
        var existingRoom = context.Rooms.FirstOrDefault(r => r.Id == id && !r.IsDeleted);
        if (existingRoom == null)
            return Result<bool>.Failure(Error.NotFound("Room not found for deletion."));

        existingRoom.IsDeleted = true;
        existingRoom.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
