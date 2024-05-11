using HousingReservationSystemApi.Contracts.Accommodation;
using HousingReservationSystemApi.Contracts.Booking;
using HousingReservationSystemApplication.Interfaces;
using HousingReservationSystemDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousingReservationSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBookingAsync()
        {
            try
            {
                var booking = await _service.GetAllBookingsAsync();
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingRequest request)
        {
            var booking = Booking.Create(
                Guid.NewGuid(),
                request.UserId,
                request.AccommodationId,
                request.CheckInDate,
                request.CheckOutDate);

            var bookingId = await _service.CreateBookingAsync(booking);

            return Ok(bookingId);
        }

        //[HttpPut("Update {id}")]
        //public async Task<IActionResult> UpdateBookingAsync(Guid id, UpdateBookingRequest request)
        //{
        //    await _service.UpdateBookingAsync(id,request.UserId, request.AccommodationId, request.CheckInDate, request.CheckOutDate);
        //    return Ok();
        //}
    }
}
