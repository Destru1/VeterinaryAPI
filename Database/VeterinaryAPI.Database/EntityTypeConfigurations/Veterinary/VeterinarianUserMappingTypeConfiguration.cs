using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database.EntityTypeConfigurations.Veterinary
{
    public class VeterinarianUserMappingTypeConfiguration : IEntityTypeConfiguration<VeterinarianUserMapping>
    {
        public void Configure(EntityTypeBuilder<VeterinarianUserMapping> builder)
        {
            builder
                .HasIndex(nameof(VeterinarianUserMapping.VeterinarianId), nameof(VeterinarianUserMapping.UserId))
                .IsUnique(true);

            builder
                .HasOne(vum => vum.Veterinarian)
                .WithMany(v => v.Users)
                .HasForeignKey(vum => vum.VeterinarianId);


            builder
                .HasOne(vum => vum.User)
                .WithMany(u => u.Veterinarians)
                .HasForeignKey(vum => vum.UserId);
        }
    }
}
