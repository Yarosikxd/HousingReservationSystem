using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemDomain.Abstraction.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetByEmailUserAsync(string email);
    }
}
