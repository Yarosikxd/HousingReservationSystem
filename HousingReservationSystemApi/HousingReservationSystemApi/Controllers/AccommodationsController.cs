using HousingReservationSystemApi.Contracts.Accommodation;
using HousingReservationSystemApplication.Interfaces;
using HousingReservationSystemDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousingReservationSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationsController : ControllerBase
    {
        private readonly IAccommodationService _service;
        private readonly ILogger<AccommodationsController> _logger;

        public AccommodationsController(IAccommodationService service, ILogger<AccommodationsController> logger) 
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAccommodationAsync()
        {
            try
            {
                var accommodations = await _service.GetAllAccommodationAsync();
                _logger.LogInformation("Retrieved all accommodations successfully"); 
                return Ok(accommodations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all accommodations");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccommodationAsync([FromBody] CreateAccommodationRequest request)
        {
            try
            {
                var accommodation = Accommodation.Create(
                    Guid.NewGuid(),
                    request.Name,
                    request.Location);

                var accommodationId = await _service.CreateAccommodationAsync(accommodation);
                _logger.LogInformation("Accommodation created successfully"); 
                return Ok(accommodationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating accommodation");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Update {id}")]
        public async Task<IActionResult> UpdateAccommodationAsync(Guid id, UpdateAccommodationRequest request)
        {
            try
            {
                await _service.UpdateAccommodationAsync(id, request.Name, request.Location);
                _logger.LogInformation($"Accommodation with ID {id} updated successfully"); 
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating accommodation");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Delete {id}")]
        public async Task<IActionResult> DeleteAccommodationAsync(Guid id)
        {
            try
            {
                await _service.DeleteAccommodationAsync(id);
                _logger.LogInformation($"Accommodation with ID {id} deleted successfully"); 
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting accommodation");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
