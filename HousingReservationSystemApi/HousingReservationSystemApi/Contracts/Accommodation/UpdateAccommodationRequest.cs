using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Accommodation
{
    public record UpdateAccommodationRequest(
    [Required] Guid Id,
    [Required] string Name,
    [Required] string Location
    );
}
