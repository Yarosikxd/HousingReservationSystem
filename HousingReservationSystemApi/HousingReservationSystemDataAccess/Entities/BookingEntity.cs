namespace HousingReservationSystemDataAccess.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AccommodationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
       
    }
}
