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
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to create booking", ex);
            }
        }

        public async Task<Guid> DeleteBookingAsync(Guid bookingId)
        {
            try
            {
                var bookingEntity = await _context.Bookings.FindAsync(bookingId);
                if (bookingEntity != null)
                {
                    _context.Bookings.Remove(bookingEntity);
                    await _context.SaveChangesAsync();
                }

                return bookingId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete booking with id {bookingId}", ex);
            }
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            try
            {
                var bookingEntities = await _context.Bookings.ToListAsync();
                return bookingEntities.Select(b => _mapper.Map<Booking>(b)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve all bookings", ex);
            }
        }

        public async Task<Booking> GetBookingByIdAsync(Guid bookingId)
        {
            try
            {
                BookingEntity bookingEntity = await _context.Bookings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == bookingId);

                Booking booking = _mapper.Map<Booking>(bookingEntity);

                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve booking with id {bookingId}", ex);
            }
        }

        public async Task<Guid> UpdateBookingAsync(Guid id, Guid userId, Guid accommodationId, DateTime chekInDate, DateTime checkOutDate)
        {
            try
            {
                var bookingEntity = await _context.Bookings.FindAsync(id);
                if (bookingEntity != null)
                {
                    bookingEntity.UserId = userId;
                    bookingEntity.AccommodationId = accommodationId;
                    bookingEntity.CheckInDate = chekInDate;
                    bookingEntity.CheckOutDate = checkOutDate;
                    await _context.SaveChangesAsync();
                }

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update booking with id {id}", ex);
            }
        }
    }
}
