using HousingReservationSystemDataAccess.Configurations;
using HousingReservationSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HousingReservationSystemDataAccess
{
    public class DataBaseDbContext : DbContext
    {
        public DataBaseDbContext(DbContextOptions<DataBaseDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AccommodationEntity> Accommodations { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AccommodationConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
        }
    }
}
