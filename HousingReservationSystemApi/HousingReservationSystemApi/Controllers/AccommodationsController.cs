using HousingReservationSystemApi.Contracts.Accommodation;
using HousingReservationSystemApplication.Services;
using HousingReservationSystemDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousingReservationSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationsController : ControllerBase
    {
        private readonly AccommodationService _accommodationService;

        public AccommodationsController(AccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAccommodationAsync()
        {
            try
            {
                var accommodations = await _accommodationService.GetAllAccommodationAsync();
                return Ok(accommodations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccommodationAsync([FromBody]CreateAccommodationRequest request)
        {
            var accommodation = Accommodation.Create(
                Guid.NewGuid(),
                request.Name,
                request.Location);

            var accommodationId = await _accommodationService.CreateAccommodationAsync(accommodation);

            return Ok(accommodationId);
        }

        [HttpPut("Update {id}")]
        public async Task<IActionResult> UpdateAccommodationAsync(Guid id, UpdateAccommodationRequest request)
        {
            await _accommodationService.UpdateAccommodationAsync(id, request.Name, request.Location);
            return Ok();
        }

        [HttpDelete("Delete {id}")]
        public async Task<IActionResult> DeleteAccommodationAsync(Guid id)
        {
            await _accommodationService.DeleteAccommodationAsync(id);
            return Ok();
        }

    }
}
