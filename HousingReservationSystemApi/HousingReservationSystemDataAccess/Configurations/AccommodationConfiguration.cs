using HousingReservationSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HousingReservationSystemDataAccess.Configurations
{
    public partial class AccommodationConfiguration : IEntityTypeConfiguration<AccommodationEntity>
    {
        public void Configure(EntityTypeBuilder<AccommodationEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired();

            builder.Property(a => a.Location)
                .IsRequired();
        }
    }
}
