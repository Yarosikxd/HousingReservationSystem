namespace HousingReservationSystemApi.Contracts.Booking
{
    public record BookingResponse(
    Guid Id,
    Guid UserId,
    Guid AccommodationId,
    DateTime CheckInDate,
    DateTime CheckOutDate
    );
}
