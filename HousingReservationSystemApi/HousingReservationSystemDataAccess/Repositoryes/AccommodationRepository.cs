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
            try
            {
                var accommodationEntities = await _context.Accommodations
                    .AsNoTracking()
                    .ToListAsync();

                var accommodations = accommodationEntities
                    .Select(a => Accommodation.Create(a.Id, a.Name, a.Location))
                    .ToList();

                return accommodations;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get all accommodations", ex);
            }
        }

        public async Task<Accommodation> GetAccommodationByIdAsync(Guid accommodationId)
        {
            try
            {
                AccommodationEntity accommodationEntity = await _context.Accommodations
                   .AsNoTracking()
                   .FirstOrDefaultAsync(a => a.Id == accommodationId);

                Accommodation accommodation = _mapper.Map<Accommodation>(accommodationEntity);

                return accommodation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get accommodation with id {accommodationId}", ex);
            }
        }

        public async Task<Guid> CreateAccommodationAsync(Accommodation accommodation)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to create accommodation", ex);
            }
        }

        public async Task<Guid> UpdateAccommodationAsync(Guid id, string name, string location)
        {
            try
            {
                var accommodation = await _context.Accommodations.FindAsync(id);
                if (accommodation != null)
                {
                    accommodation.Name = name;
                    accommodation.Location = location;
                    await _context.SaveChangesAsync();
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update accommodation with id {id}", ex);
            }
        }

        public async Task<Guid> DeleteAccommodationAsync(Guid id)
        {
            try
            {
                var accommodation = await _context.Accommodations.FindAsync(id);
                if (accommodation != null)
                {
                    _context.Accommodations.Remove(accommodation);
                    await _context.SaveChangesAsync();
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete accommodation with id {id}", ex);
            }
        }
    }

}
