using AutoMapper;
using HousingReservationSystemDataAccess.Entities;
using HousingReservationSystemDomain.Models;

namespace HousingReservationSystemDataAccess
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<UserEntity, User>();
            CreateMap<AccommodationEntity, Accommodation>();
            CreateMap<BookingEntity, Booking>();
        }
    }
}
