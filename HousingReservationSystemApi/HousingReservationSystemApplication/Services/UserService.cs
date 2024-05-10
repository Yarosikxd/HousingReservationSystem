using HousingReservationSystemDomain.Abstraction.Auth;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemApplication.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email);

            await _userRepository.AddUserAsync(user);
        }

        public async Task<string>Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailUserAsync(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if(result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
