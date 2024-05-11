using AutoMapper;
using HousingReservationSystemDataAccess.Entities;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace HousingReservationSystemDataAccess.Repositoryes
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataBaseDbContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(DataBaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> CreateBookingAsync(Booking booking)
        {
            var bookingId = Guid.NewGuid(); 

            var bookingEntity = new BookingEntity
            {
                Id = bookingId,
                UserId = booking.UserId,
                AccommodationId = booking.AccommodationId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate
            };

            await _context.Bookings.AddAsync(bookingEntity);
            await _context.SaveChangesAsync();

            return bookingEntity.Id;
        }

        public async Task<Guid> DeleteBookingAsync(Guid bookingId)
        {
            var bookingEntity = await _context.Bookings.FindAsync(bookingId);
            if (bookingEntity != null)
            {
                _context.Bookings.Remove(bookingEntity);
                await _context.SaveChangesAsync();
            }

            return bookingId;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            var bookingEntities = await _context.Bookings.ToListAsync();
            return bookingEntities.Select(b => Booking.Create(
                b.Id, 
                b.UserId, 
                b.AccommodationId,
                b.CheckInDate, 
                b.CheckOutDate))
                .ToList();
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
           BookingEntity bookingEntity = await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            Booking booking = _mapper.Map<Booking>(bookingEntity);

            return booking;
        }

        public async Task<Guid> UpdateBookingAsync(Booking booking)
        {
            var bookingEntity = await _context.Bookings.FindAsync(booking.Id);
            if (bookingEntity != null)
            {
                bookingEntity.UserId = booking.UserId;
                bookingEntity.AccommodationId = booking.AccommodationId;
                bookingEntity.CheckInDate = booking.CheckInDate;
                bookingEntity.CheckOutDate = booking.CheckOutDate;
                await _context.SaveChangesAsync();
            }

            return booking.Id;
        }
    }
}
