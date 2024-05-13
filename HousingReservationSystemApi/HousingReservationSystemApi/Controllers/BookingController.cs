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
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService service, ILogger<BookingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBookingAsync()
        {
            try
            {
                var booking = await _service.GetAllBookingsAsync();
                _logger.LogInformation("Retrieved all bookings successfully"); 
                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all bookings");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingRequest request)
        {
            try
            {
                var booking = Booking.Create(
                    Guid.NewGuid(),
                    request.UserId,
                    request.AccommodationId,
                    request.CheckInDate,
                    request.CheckOutDate);

                var bookingId = await _service.CreateBookingAsync(booking);
                _logger.LogInformation("Booking created successfully"); 
                return Ok(bookingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update {id}")]
        public async Task<IActionResult> UpdateBookingAsync(Guid id, UpdateBookingRequest request)
        {
            try
            {
                await _service.UpdateBookingAsync(id, request.UserId, request.AccommodationId, request.CheckInDate, request.CheckOutDate);
                _logger.LogInformation($"Booking with ID {id} updated successfully"); 
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete {id}")]
        public async Task<IActionResult> DeleteBookingAsync(Guid id)
        {
            try
            {
                await _service.DeleteBookingAsync(id);
                _logger.LogInformation($"Booking with ID {id} deleted successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
