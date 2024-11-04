using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Extensions.MapperExtensions;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Booking;

public class BookingService : IBookingService
{
    private readonly DataContext context;

    public BookingService(DataContext context)
    {
        this.context = context;
    }

    public Result<PaginationResponse<IEnumerable<BookingReadDto>>> GetAllBookings(BookingFilter filter)
    {
        var bookings = context.Bookings.AsQueryable();

        if (filter.ClientId != null)
            bookings = bookings.Where(b => b.ClientId == filter.ClientId);
        if (filter.ClassId != null)
            bookings = bookings.Where(b => b.ClassId == filter.ClassId);

        int totalRecords = bookings.Count();
        var result = bookings.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Where(b => !b.IsDeleted)
            .Select(b => b.BookingToReadDto())
            .ToList();

        var paginationResponse = PaginationResponse<IEnumerable<BookingReadDto>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
        return Result<PaginationResponse<IEnumerable<BookingReadDto>>>.Success(paginationResponse);
    }

    public Result<BookingReadDto> GetBookingById(int id)
    {
        var booking = context.Bookings
            .Where(b => !b.IsDeleted && b.Id == id)
            .Select(b => b.BookingToReadDto())
            .FirstOrDefault();

        return booking == null
            ? Result<BookingReadDto>.Failure(Error.NotFound("Booking with the specified ID was not found."))
            : Result<BookingReadDto>.Success(booking);
    }

    public Result<bool> CreateBooking(BookingCreateDto createDto)
    {
        context.Bookings.Add(createDto.CreateDtoToBooking());
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not saved!"));
    }

    public Result<bool> UpdateBooking(BookingUpdateDto updateDto)
    {
        var booking = context.Bookings.FirstOrDefault(b => !b.IsDeleted && b.Id == updateDto.Id);
        if (booking == null)
            return Result<bool>.Failure(Error.NotFound("Booking not found for update."));

        booking.UpdateDtoToBooking(updateDto);
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not updated!"));
    }

    public Result<bool> DeleteBooking(int id)
    {
        var booking = context.Bookings.FirstOrDefault(b => b.Id == id && !b.IsDeleted);
        if (booking == null)
            return Result<bool>.Failure(Error.NotFound("Booking not found for deletion."));

        booking.IsDeleted = true;
        booking.DeletedAt = DateTime.UtcNow;
        int res = context.SaveChanges();

        return res > 0
            ? Result<bool>.Success(true)
            : Result<bool>.Failure(Error.InternalServerError("Data not deleted!"));
    }
}
