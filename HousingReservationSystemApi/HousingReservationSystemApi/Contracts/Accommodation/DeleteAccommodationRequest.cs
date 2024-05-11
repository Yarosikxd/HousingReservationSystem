using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Accommodation
{
    public record DeleteAccommodationRequest(
    [Required] Guid Id
    );
}
