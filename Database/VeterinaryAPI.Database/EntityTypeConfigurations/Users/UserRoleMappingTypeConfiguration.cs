using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryAPI.Database.Models.Users;

namespace VeterinaryAPI.Database.EntityTypeConfigurations.Users
{
    public class UserRoleMappingTypeConfiguration : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder
                .HasIndex(nameof(UserRoleMapping.UserId), nameof(UserRoleMapping.RoleId))
                .IsUnique(true);

            builder
                .HasOne(urm => urm.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(urm => urm.UserId);

            builder
                .HasOne(urm => urm.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(urm => urm.RoleId);
        }
    }
}
