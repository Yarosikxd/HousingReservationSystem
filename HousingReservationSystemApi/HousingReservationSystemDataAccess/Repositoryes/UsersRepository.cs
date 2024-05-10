using AutoMapper;
using HousingReservationSystemDataAccess.Entities;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace HousingReservationSystemDataAccess.Repositoryes
{
    public class UsersRepository : IUserRepository
    {
        private readonly DataBaseDbContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(DataBaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddUserAsync(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
            };
            
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailUserAsync(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            return _mapper.Map<User>(userEntity);
        }
    }
}
