using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database.EntityTypeConfigurations.Veterinary
{
    public class VeterinarianPetMappingTypeConfiguration : IEntityTypeConfiguration<VeterinarianPetMapping>
    {

        public void Configure(EntityTypeBuilder<VeterinarianPetMapping> builder)
        {

            builder
                .HasIndex(nameof(VeterinarianPetMapping.VeterinarianId), nameof(VeterinarianPetMapping.PetId))
                .IsUnique(true);

            builder
                .HasOne(vpm => vpm.Veterinarian)
                .WithMany(v => v.Pets)
                .HasForeignKey(vpm => vpm.VeterinarianId);


            builder
                .HasOne(vpm => vpm.Pet)
                .WithMany(p => p.Veterinarians)
                .HasForeignKey(vpm => vpm.PetId);


        }
    }
}
