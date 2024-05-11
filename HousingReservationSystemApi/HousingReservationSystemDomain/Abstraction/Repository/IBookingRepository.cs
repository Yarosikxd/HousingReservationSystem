using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemDomain.Abstraction.Repository
{
    public interface IBookingRepository
    {
        Task<Guid> CreateBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(Guid bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Guid> UpdateBookingAsync(Booking booking);
        Task<Guid> DeleteBookingAsync(Guid bookingId);
    }
}
