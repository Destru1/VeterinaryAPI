using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database.EntityTypeConfigurations.Veterinary
{
    public class PetTypeConfiguration : IEntityTypeConfiguration<OwnerPetMapping>
    {
        public void Configure(EntityTypeBuilder<OwnerPetMapping> builder)
        {
            builder
                .HasIndex(nameof(OwnerPetMapping.OwnerId), nameof(VeterinarianPetMapping.PetId))
                .IsUnique(true);

            builder
                .HasOne(opm => opm.Owner)
                .WithMany(o => o.Pets)
                .HasForeignKey(opm => opm.OwnerId);


            builder
                .HasOne(opm => opm.Pet)
                .WithMany(p => p.Owners)
                .HasForeignKey(opm => opm.PetId);
        }
    }
}
