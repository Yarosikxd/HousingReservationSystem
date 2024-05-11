using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemApplication.Interfaces
{
    public interface IBookingService
    {
        Task<Guid> CreateBookingAsync(Booking booking);
        Task<Booking> GetBookingByIdAsync(Guid bookingId);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Guid> UpdateBookingAsync(Guid id,Booking booking);
        Task<Guid> DeleteBookingAsync(Guid bookingId);
    }
}
