using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class VeterinarianService : BaseService<Veterinarian>, IVeterinarianService
    {
        public VeterinarianService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext,mapper)
        {

        }


        public async Task<T> GetAllAsync<T>()
        {
            List<Veterinarian> veterinarians = await this.DbSet
                   .OrderBy(v => v.FirstName)
                   .ThenBy(v => v.LastName)
                   .ToListAsync();

            T mappedVeterinarians = this.Mapper.Map<T>(veterinarians);

            return mappedVeterinarians;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Veterinarian veterinarian = await this.DbSet
                 .SingleOrDefaultAsync(v => v.Id == id);

            T mappedVeterinarian = this.Mapper.Map<T>(veterinarian);

            return mappedVeterinarian;
        }

      
       

        public async Task<Veterinarian> AddAsync(PostVeterinarianDTO veterinarian)
        {
            Veterinarian veterinarianToAdd = this.Mapper.Map<Veterinarian>(veterinarian);

            await this.DbSet.AddAsync(veterinarianToAdd);
            await this.Dbcontext.SaveChangesAsync();

            return veterinarianToAdd;
        }


        public async Task<bool> UpdateAsync(Guid id, PutVeterinarianDTO veterinarian)
        {
            Veterinarian veterinarianToUpdate = await this.GetByIdAsync<Veterinarian>(id);

            if (veterinarianToUpdate == null)
            {
                throw new Exception("Vet does not exist");
            }

            Veterinarian updatedVeterinarian = this.Mapper.Map(veterinarian, veterinarianToUpdate);
            updatedVeterinarian.UpdatedOn = DateTime.UtcNow;

            this.Dbcontext.Update(updatedVeterinarian);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Veterinarian veterinarianToDelete = await this.GetByIdAsync<Veterinarian>(id);

            if (veterinarianToDelete == null)
            {
                return false;
            }

            this.DbSet.Remove(veterinarianToDelete);

            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

    }
}
