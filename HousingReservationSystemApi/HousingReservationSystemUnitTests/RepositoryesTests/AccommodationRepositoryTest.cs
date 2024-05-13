//using AutoMapper;
//using HousingReservationSystemDataAccess.Entities;
//using HousingReservationSystemDataAccess.Repositoryes;
//using HousingReservationSystemDataAccess;
//using HousingReservationSystemDomain.Models;
//using Microsoft.EntityFrameworkCore;
//using Moq;


//namespace HousingReservationSystemUnitTests.RepositoryesTests
//{
//    public class AccommodationRepositoryTests
//    {
//        private readonly Mock<DataBaseDbContext> _mockDbContext;
//        private readonly IMapper _mapper;

//        public AccommodationRepositoryTests()
//        {
//            // Arrange: Setting up AutoMapper
//            var mapperConfig = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<AccommodationEntity, Accommodation>();
//                cfg.CreateMap<Accommodation, AccommodationEntity>();
//            });

//            _mapper = mapperConfig.CreateMapper();

//            // Arrange: Setting up Mock DbContext
//            var accommodationEntities = new List<AccommodationEntity>
//            {
//                new AccommodationEntity { Id = Guid.NewGuid(), Name = "Accommodation 1", Location = "Location 1" },
//                new AccommodationEntity { Id = Guid.NewGuid(), Name = "Accommodation 2", Location = "Location 2" }
//            }.AsQueryable();

//            var mockDbSet = new Mock<DbSet<AccommodationEntity>>();
//            mockDbSet.As<IQueryable<AccommodationEntity>>().Setup(m => m.Provider).Returns(accommodationEntities.Provider);
//            mockDbSet.As<IQueryable<AccommodationEntity>>().Setup(m => m.Expression).Returns(accommodationEntities.Expression);
//            mockDbSet.As<IQueryable<AccommodationEntity>>().Setup(m => m.ElementType).Returns(accommodationEntities.ElementType);
//            mockDbSet.As<IQueryable<AccommodationEntity>>().Setup(m => m.GetEnumerator()).Returns(accommodationEntities.GetEnumerator());

//            _mockDbContext = new Mock<DataBaseDbContext>();
//            _mockDbContext.Setup(x => x.Accommodations).Returns(mockDbSet.Object);
//        }

//        [Fact]
//        public async Task GetAllAccommodationAsync_ReturnsAllAccommodations()
//        {
//            // Arrange
//            var repository = new AccommodationRepository(_mockDbContext.Object, _mapper);

//            // Act
//            var accommodations = await repository.GetAllAccommodationAsync();

//            // Assert
//            Assert.NotNull(accommodations);
//            Assert.Equal(2, accommodations.Count);
//        }

//        [Fact]
//        public async Task GetAccommodationByIdAsync_ValidId_ReturnsAccommodation()
//        {
//            // Arrange
//            var accommodationId = Guid.NewGuid();
//            var accommodationName = "Accommodation 1";
//            var repository = new AccommodationRepository(_mockDbContext.Object, _mapper);

//            // Act
//            var accommodation = await repository.GetAccommodationByIdAsync(accommodationId);

//            // Assert
//            Assert.NotNull(accommodation);
//            Assert.Equal(accommodationId, accommodation.Id);
//            Assert.Equal(accommodationName, accommodation.Name);
//        }

//        [Fact]
//        public async Task UpdateAccommodationAsync_ValidId_UpdatesAccommodation()
//        {
//            // Arrange
//            var repository = new AccommodationRepository(_mockDbContext.Object, _mapper);
//            var accommodationId = Guid.NewGuid();
//            var newName = "Updated Accommodation Name";
//            var newLocation = "Updated Location";

//            // Act
//            await repository.UpdateAccommodationAsync(accommodationId, newName, newLocation);
//            var updatedAccommodation = await repository.GetAccommodationByIdAsync(accommodationId);

//            // Assert
//            Assert.NotNull(updatedAccommodation);
//            Assert.Equal(newName, updatedAccommodation.Name);
//            Assert.Equal(newLocation, updatedAccommodation.Location);
//        }

//        [Fact]
//        public async Task DeleteAccommodationAsync_ValidId_DeletesAccommodation()
//        {
//            // Arrange
//            var repository = new AccommodationRepository(_mockDbContext.Object, _mapper);
//            var accommodationId = Guid.NewGuid();

//            // Act
//            await repository.DeleteAccommodationAsync(accommodationId);
//            var deletedAccommodation = await repository.GetAccommodationByIdAsync(accommodationId);

//            // Assert
//            Assert.Null(deletedAccommodation);
//        }
//    }
//}
