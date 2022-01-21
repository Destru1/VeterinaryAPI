using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database
{
    public class VeterinaryAPIDbcontext : DbContext
    {
        private readonly ApplicationSettings options;

        public VeterinaryAPIDbcontext(IOptions<ApplicationSettings> options)
        {
            this.options = options.Value;
        }

        public DbSet<Veterinarian> Veterinarians { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }


        public DbSet<OwnerPetMapping> OwnerPetMappings { get; set; }
        public DbSet<VeterinarianPetMapping> VeterinariansPetsMapping { get; set; }

        public DbSet<VeterinarianPositionMapping> VeterinariansPositionsMapping { get; set; }

        public DbSet<VeterinarianUserMapping> VeterinariansUsersMapping { get; set; }

        public DbSet<UserRoleMapping> UserRoleMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(options.DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
