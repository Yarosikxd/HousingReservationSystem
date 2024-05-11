using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemApplication.Interfaces
{
    public interface IAccommodationService
    {
        Task<List<Accommodation>> GetAllAccommodationAsync();
        Task<Accommodation> GetAccommodationByIdAsync(Guid accommodationId);
        Task<Guid> CreateAccommodationAsync(Accommodation accommodation);
        Task<Guid> UpdateAccommodationAsync(Guid id, string name, string location);
        Task<Guid> DeleteAccommodationAsync(Guid id);
    }
}
