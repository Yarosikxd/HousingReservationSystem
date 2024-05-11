using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemApplication.Services
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository _repository;

        public AccommodationService(IAccommodationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Accommodation>> GetAllAccommodationAsync()
        {
            return await _repository.GetAllAccommodationAsync();
        }

        public async Task<Accommodation> GetAccommodationByIdAsync(Guid accommodationId)
        {
            return await _repository.GetAccommodationByIdAsync(accommodationId);
        }

        public async Task<Guid> CreateAccommodationAsync(Accommodation accommodation)
        {
            return await _repository.CreateAccommodationAsync(accommodation);
        }

        public async Task<Guid> UpdateAccommodationAsync(Guid id, string name, string location)
        {
            return await _repository.UpdateAccommodationAsync(id, name, location);
        }

        public async Task<Guid> DeleteAccommodationAsync(Guid id)
        {
            return await _repository.DeleteAccommodationAsync(id);
        }
    }
}
