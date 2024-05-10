namespace HousingReservationSystemDomain.Models
{
    public class Booking
    {
        private Booking(Guid id, Guid userId, Guid accommodationId, DateTime checkInDate, DateTime checkOutDate)
        {
            Id = id;
            UserId = userId;
            AccommodationId = accommodationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public Guid AccommodationId { get; private set; }

        public DateTime CheckInDate { get; private set; }

        public DateTime CheckOutDate { get; private set; }

        public static Booking Create(Guid id, Guid userId, Guid accommodationId, DateTime checkInDate, DateTime checkOutDate)
        {
            return new Booking(id, userId, accommodationId, checkInDate, checkOutDate);
        }
    }
}
