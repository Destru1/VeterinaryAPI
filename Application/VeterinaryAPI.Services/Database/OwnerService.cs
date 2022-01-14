using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Owner;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class OwnerService : BaseService<Owner>, IOwnerService
    {
        public OwnerService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext,mapper)
        {

        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Owner> owners = await this.DbSet
                .OrderBy(o => o.FirstName)
                .ThenBy(o => o.LastName)
                .Include(o => o.Pets)
                .ToListAsync();

            T mappedOwners = this.Mapper.Map<T>(owners);

            return mappedOwners;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Owner owner = await this.DbSet
                .SingleOrDefaultAsync(o => o.Id == id);

            T mappedOwner = this.Mapper.Map<T>(owner);

            return mappedOwner;
        }


        public async Task<T> AddAsync<T>(PostOwnerDTO owner)
        {
            Owner ownerToAdd = this.Mapper.Map<Owner>(owner);

            await this.DbSet.AddAsync(ownerToAdd);
            await this.Dbcontext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(ownerToAdd);

            return result;
        }

        public async Task<bool> UpdateAsync(Guid id, PutOwnerDTO owner)
        {
            Owner ownerToUpdate = await this.GetByIdAsync<Owner>(id);

            if (ownerToUpdate == null)
            {
                throw new Exception("Owner dosent exist.");
            }

            Owner updatedOwner = this.Mapper.Map(owner, ownerToUpdate);
            updatedOwner.UpdatedOn = DateTime.UtcNow;

            this.DbSet.Update(updatedOwner);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Owner ownerToDelete = await this.GetByIdAsync<Owner>(id);

            if (ownerToDelete == null)
            {
                return false;
            }

            this.DbSet.Remove(ownerToDelete);

            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

    }
}
