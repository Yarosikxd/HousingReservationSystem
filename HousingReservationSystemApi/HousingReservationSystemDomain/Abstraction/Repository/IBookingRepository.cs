using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemDomain.Abstraction.Repository
{
    public interface IBookingRepository
    {
        Task<Guid> CreateBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(Guid bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Guid> UpdateBookingAsync(Guid id, Guid userId, Guid accommodationId, DateTime chekInDate, DateTime checkOutDate);
        Task<Guid> DeleteBookingAsync(Guid bookingId);
    }
}
