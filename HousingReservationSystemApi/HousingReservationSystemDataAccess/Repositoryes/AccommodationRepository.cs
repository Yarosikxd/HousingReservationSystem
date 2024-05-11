using AutoMapper;
using HousingReservationSystemDataAccess.Entities;
using HousingReservationSystemDomain.Abstraction.Repository;
using HousingReservationSystemDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace HousingReservationSystemDataAccess.Repositoryes
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly DataBaseDbContext _context;
        private readonly IMapper _mapper;

        public AccommodationRepository(DataBaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Accommodation>> GetAllAccommodationAsync()
        {
            var accommodationEntities = await _context.Accommodations
                .AsNoTracking()
                .ToListAsync();

            var accommodations = accommodationEntities
                .Select(a => Accommodation.Create(a.Id, a.Name, a.Location))
                .ToList();

            return accommodations;
        }

        public async Task<Accommodation> GetAccommodationByIdAsync(Guid accommodationId)
        {
            AccommodationEntity accommodationEntity = await _context.Accommodations
           .AsNoTracking()
           .FirstOrDefaultAsync(a => a.Id == accommodationId);

            Accommodation accommodation = _mapper.Map<Accommodation>(accommodationEntity);

            return accommodation;
        }

        public async Task<Guid> CreateAccommodationAsync(Accommodation accommodation)
        {
            var accommodationId = Guid.NewGuid();

            var accommodationEntity = new AccommodationEntity
            {
                Id = accommodationId,
                Location = accommodation.Location,
                Name = accommodation.Name,
            };

            await _context.Accommodations.AddAsync(accommodationEntity);
            await _context.SaveChangesAsync();

            return accommodationEntity.Id;
        }

        public async Task<Guid> UpdateAccommodationAsync (Guid id, string name, string location)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);
            if(accommodation != null)
            {
                accommodation.Name = name;
                accommodation.Location = location;
                await _context.SaveChangesAsync();
            }
            return id;
        }

        public async Task<Guid> DeleteAccommodationAsync(Guid id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);
            if(accommodation != null)
            {
                _context.Accommodations.Remove(accommodation);
                await _context.SaveChangesAsync();
            }
            return id;
        }
    }
}
