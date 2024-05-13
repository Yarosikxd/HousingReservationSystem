using AutoMapper;
using HousingReservationSystemDataAccess.Entities;
using HousingReservationSystemDataAccess.Repositoryes;
using HousingReservationSystemDataAccess;
using HousingReservationSystemDomain.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using HousingReservationSystemUnitTests.RepositoryesTests.DbSet;

namespace HousingReservationSystemUnitTests.RepositoryesTests
{
    public class BookingRepositoryTests
    {
        private readonly Mock<DataBaseDbContext> _mockDbContext;
        private readonly IMapper _mapper;

        public BookingRepositoryTests()
        {
            // Arrange: Setting up AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookingEntity, Booking>();
                cfg.CreateMap<Booking, BookingEntity>();
            });

            _mapper = mapperConfig.CreateMapper();

            // Arrange: Setting up Mock DbContext
            var bookingEntities = new List<BookingEntity>
        {
            new BookingEntity { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), AccommodationId = Guid.NewGuid(), CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(1) },
            new BookingEntity { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), AccommodationId = Guid.NewGuid(), CheckInDate = DateTime.Now.AddDays(2), CheckOutDate = DateTime.Now.AddDays(3) }
        }.AsQueryable();

            var mockDbSet = new Mock<DbSet<BookingEntity>>();
            mockDbSet.SetupQueryableData(bookingEntities.ToList());

            _mockDbContext = new Mock<DataBaseDbContext>();
            _mockDbContext.Setup(x => x.Bookings).Returns(mockDbSet.Object);
        }

        [Fact]
        public async Task GetAllBookingsAsync_ReturnsAllBookings()
        {
            // Arrange
            var repository = new BookingRepository(_mockDbContext.Object, _mapper);

            // Act
            var bookings = await repository.GetAllBookingsAsync();

            // Assert
            Assert.NotNull(bookings);
            Assert.Equal(2, bookings.Count);
        }

        [Fact]
        public async Task GetBookingByIdAsync_ValidId_ReturnsBooking()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var repository = new BookingRepository(_mockDbContext.Object, _mapper);

            // Act
            var booking = await repository.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.NotNull(booking);
            Assert.Equal(bookingId, booking.Id);
        }

        [Fact]
        public async Task CreateBookingAsync_ValidBooking_CreatesBooking()
        {
            // Arrange
            var repository = new BookingRepository(_mockDbContext.Object, _mapper);
            var newBooking =  Booking.Create(
                id: Guid.NewGuid(),
                userId: Guid.NewGuid(),
                accommodationId: Guid.NewGuid(),
                checkInDate: DateTime.Now.AddDays(4),
                checkOutDate: DateTime.Now.AddDays(5)
                );

            // Act 
            var bookingId = await repository.CreateBookingAsync(newBooking);
            var createdBooking = await repository.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.NotNull(createdBooking);
            Assert.Equal(newBooking.UserId, createdBooking.UserId);
            Assert.Equal(newBooking.AccommodationId, createdBooking.AccommodationId);
            Assert.Equal(newBooking.CheckInDate, createdBooking.CheckInDate);
            Assert.Equal(newBooking.CheckOutDate, createdBooking.CheckOutDate);
        }

        [Fact]
        public async Task UpdateBookingAsync_ValidId_UpdatesBooking()
        {
            // Arrange
            var repository = new BookingRepository(_mockDbContext.Object, _mapper);
            var bookingId = Guid.NewGuid();
            var newUserId = Guid.NewGuid();
            var newAccommodationId = Guid.NewGuid();
            var newCheckInDate = DateTime.Now.AddDays(6);
            var newCheckOutDate = DateTime.Now.AddDays(7);

            // Act
            await repository.UpdateBookingAsync(bookingId, newUserId, newAccommodationId, newCheckInDate, newCheckOutDate);
            var updatedBooking = await repository.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.NotNull(updatedBooking);
            Assert.Equal(newUserId, updatedBooking.UserId);
            Assert.Equal(newAccommodationId, updatedBooking.AccommodationId);
            Assert.Equal(newCheckInDate, updatedBooking.CheckInDate);
            Assert.Equal(newCheckOutDate, updatedBooking.CheckOutDate);
        }

        [Fact]
        public async Task DeleteBookingAsync_ValidId_DeletesBooking()
        {
            // Arrange
            var repository = new BookingRepository(_mockDbContext.Object, _mapper);
            var bookingId = Guid.NewGuid();

            // Act
            await repository.DeleteBookingAsync(bookingId);
            var deletedBooking = await repository.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.Null(deletedBooking);
        }
    }
}
