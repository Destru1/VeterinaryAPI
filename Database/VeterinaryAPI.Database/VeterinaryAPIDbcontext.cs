using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database
{
    public class VeterinaryAPIDbcontext : DbContext
    {


        public DbSet<Veterinarian> Veterinarians { get; set; }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Position> Positions { get; set; }




        public DbSet<VeterinarianPetMapping> VeterinariansPetsMapping { get; set; }
        public DbSet<OwnerPetMapping> OwnerPetMappings { get; set; }

        public DbSet<VeterinarianPositionMapping> VeterinariansPositionsMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server =.; Database =VeterinaryAPI; Integrated Security = true; ");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
