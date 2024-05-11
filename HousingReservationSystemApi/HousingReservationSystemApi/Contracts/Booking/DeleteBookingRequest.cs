using System.ComponentModel.DataAnnotations;

namespace HousingReservationSystemApi.Contracts.Booking
{
    public record DeleteBookingRequest(
    [Required] Guid BookingId
    );
}
