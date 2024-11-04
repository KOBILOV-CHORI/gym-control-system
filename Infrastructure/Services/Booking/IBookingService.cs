using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;
using MiniAPI.Extensions.PatternResultExtensions;

namespace Infrastructure.Services.Booking;

public interface IBookingService
{
    Result<PaginationResponse<IEnumerable<BookingReadDto>>> GetAllBookings(BookingFilter filter);
    Result<BookingReadDto> GetBookingById(int id);
    Result<bool> CreateBooking(BookingCreateDto createDto);
    Result<bool> UpdateBooking(BookingUpdateDto updateDto);
    Result<bool> DeleteBooking(int id);
}