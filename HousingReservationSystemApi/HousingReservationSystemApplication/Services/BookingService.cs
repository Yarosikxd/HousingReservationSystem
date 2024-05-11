using HousingReservationSystemApplication.Interfaces;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemApplication.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repository;

        public BookingService(IBookingRepository repository)
        {
            _repository = repository;
        }

      public async Task<Guid> CreateBookingAsync(Booking booking)
      {
            return  await _repository.CreateBookingAsync(booking);
      }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            return await _repository.GetBookingByIdAsync(bookingId);
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _repository.GetAllBookingsAsync();
        }

        public async Task<Guid> UpdateBookingAsync(Guid id, Guid userId, Guid accommodationId, DateTime chekInDate, DateTime checkOutDate)
        {
            return await _repository.UpdateBookingAsync(id,userId,accommodationId,chekInDate,checkOutDate);
        }

        public async Task<Guid> DeleteBookingAsync(Guid bookingId)
        {
            return await _repository.DeleteBookingAsync(bookingId);
        }
    }
}
