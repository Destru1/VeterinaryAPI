using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Pet;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
   public class PetService : BaseService<Pet>, IPetService
    {

        public PetService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext,mapper)
        {

        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Pet> pets = await this.DbSet
                .OrderBy(p => p.Type)
                .ThenBy(p => p.Breed)
                .ThenBy(p => p.Name)
                .ToListAsync();

            T mappedPets = this.Mapper.Map<T>(pets);

            return mappedPets;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Pet pet = await this.DbSet
                 .SingleOrDefaultAsync(pet => pet.Id == id);

            T mappedPet = this.Mapper.Map<T>(pet);

            return mappedPet;
        }

        public async Task<T> AddAsync<T>(PostPetDTO pet)
        {
            Pet petToAdd = this.Mapper.Map<Pet>(pet);

            await this.DbSet.AddAsync(petToAdd);
            await this.Dbcontext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(petToAdd);

            return result;
        }

        public async Task<bool> UpdateAsync(Guid id, PutPetDTO pet)
        {
            Pet petToUpdate = await this.GetByIdAsync<Pet>(id);

            if (petToUpdate == null)
            {
                throw new Exception("Pet does not exist");
            }

            Pet updatedPet = this.Mapper.Map(pet, petToUpdate);
            updatedPet.UpdatedOn = DateTime.UtcNow;

            this.DbSet.Update(updatedPet);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Pet petToDelete = await this.GetByIdAsync<Pet>(id);

            if (petToDelete == null)
            {
                return false;
            }

            this.DbSet.Remove(petToDelete);

            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

    }
}
