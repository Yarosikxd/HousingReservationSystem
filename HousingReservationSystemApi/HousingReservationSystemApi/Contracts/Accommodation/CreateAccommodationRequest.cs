using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Accommodation
{
    public record CreateAccommodationRequest(
    [Required] string Name,
    [Required] string Location
    );
}
