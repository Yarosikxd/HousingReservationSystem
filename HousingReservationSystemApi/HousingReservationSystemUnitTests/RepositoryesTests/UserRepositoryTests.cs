using AutoMapper;
using HousingReservationSystemDataAccess;
using HousingReservationSystemDataAccess.Entities;
using HousingReservationSystemDataAccess.Repositoryes;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace HousingReservationSystemUnitTests.RepositoryesTests
{
    public class UserRepositoryTests
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly DbContextOptions<DataBaseDbContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, User>();
                cfg.CreateMap<User, UserEntity>();
            });

            _mapper = configurationProvider.CreateMapper();

            using (var context = new DataBaseDbContext(_options))
            {
                context.Users.Add(new UserEntity
                {
                    Id = Guid.NewGuid(),
                    UserName = "TestUser",
                    Email = "test@example.com",
                    PasswordHash = "hash"
                });
                context.SaveChanges();
            }

            var dbContext = new DataBaseDbContext(_options);
            _userRepository = new UsersRepository(dbContext, _mapper);
        }

        [Fact]
        public async Task AddUserAsync_Success()
        {
            // Arrange
            var newUser = User.Create(Guid.NewGuid(), "NewUser", "newhash", "newuser@example.com");

            // Act
            await _userRepository.AddUserAsync(newUser);

            // Assert
            using (var context = new DataBaseDbContext(_options))
            {
                var addedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
                Assert.NotNull(addedUser);
                Assert.Equal(newUser.UserName, addedUser.UserName);
                Assert.Equal(newUser.Email, addedUser.Email);
                Assert.Equal(newUser.PasswordHash, addedUser.PasswordHash);
            }
        }

        [Fact]
        public async Task GetByEmailUserAsync_Success()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var user = await _userRepository.GetByEmailUserAsync(email);

            // Assert
            Assert.NotNull(user);
            Assert.Equal("TestUser", user.UserName);
            Assert.Equal("test@example.com", user.Email);
        }

        [Fact]
        public async Task GetByEmailUserAsync_UserNotFound()
        {
            // Arrange
            var email = "notfound@example.com";

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userRepository.GetByEmailUserAsync(email));
        }
    }
}
