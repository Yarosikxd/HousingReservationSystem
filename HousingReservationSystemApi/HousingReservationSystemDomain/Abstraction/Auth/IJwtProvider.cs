using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemDomain.Abstraction.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
