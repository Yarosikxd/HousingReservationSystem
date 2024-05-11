using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Booking
{
    public record CreateBookingRequest(
    [Required] Guid UserId,
    [Required] Guid AccommodationId,
    [Required] DateTime CheckInDate,
    [Required] DateTime CheckOutDate
    );
}
