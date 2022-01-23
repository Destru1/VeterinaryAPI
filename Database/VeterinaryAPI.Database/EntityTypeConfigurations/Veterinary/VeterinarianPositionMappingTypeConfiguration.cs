using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database.EntityTypeConfigurations.Veterinary
{
    public class VeterinarianPositionMappingTypeConfiguration : IEntityTypeConfiguration<VeterinarianPositionMapping>
    {

        public void Configure(EntityTypeBuilder<VeterinarianPositionMapping> builder)
        {

            builder
                .HasIndex(nameof(VeterinarianPositionMapping.VeterinarianId), nameof(VeterinarianPositionMapping.PositionId))
                .IsUnique(true);

            builder
                .HasOne(vpm => vpm.Veterinarian)
                .WithMany(v => v.Positions)
                .HasForeignKey(vpm => vpm.VeterinarianId);


            builder
                .HasOne(vpm => vpm.Position)
                .WithMany(p => p.Veterinarians)
                .HasForeignKey(vpm => vpm.PositionId);


        }

    }
}
