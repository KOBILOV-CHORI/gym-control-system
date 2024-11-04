using Domain.Dtos;
using Domain.Filters;
using Infrastructure.PatternResultExtensions;
using Infrastructure.Services.Booking;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/bookings")]
public sealed class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetBookings([FromQuery] BookingFilter filter)
        => (bookingService.GetAllBookings(filter)).ToActionResult();

    [HttpGet("{id:int}")]
    public IActionResult GetBookingById(int id)
        => (bookingService.GetBookingById(id)).ToActionResult();
    
    [HttpPost]
    public IActionResult CreateBooking([FromBody] BookingCreateDto dto)
        => (bookingService.CreateBooking(dto)).ToActionResult();

    [HttpPut("{id:int}")]
    public IActionResult UpdateBooking(int id,[FromBody] BookingUpdateDto dto)
        => (bookingService.UpdateBooking(dto)).ToActionResult();

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBooking(int id)
        => (bookingService.DeleteBooking(id)).ToActionResult();
}