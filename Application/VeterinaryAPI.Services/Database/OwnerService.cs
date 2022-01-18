using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private readonly IPetService petService;
        private readonly IOwnerPetMappingService ownerPetMappingService;
        public OwnerService(VeterinaryAPIDbcontext dbcontext, 
            IMapper mapper,
            IPetService petService,
            IOwnerPetMappingService ownerPetMappingService)
            : base(dbcontext,mapper)
        {
            this.petService = petService;
            this.ownerPetMappingService = ownerPetMappingService;
        }

        public async Task<T> GetAllAsync<T>()
        {
            List<Owner> owners = await this.DbSet
                .OrderBy(o => o.FirstName)
                .ThenBy(o => o.LastName)
                .Include(o => o.Pets)
                .ThenInclude(o => o.Pet)
                .ToListAsync();

            T mappedOwners = this.Mapper.Map<T>(owners);

            return mappedOwners;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Owner owner = await this.DbSet
                .Include(o => o.Pets)
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

        public async Task<bool> PartialUpdateAsync(Guid id, PatchOwnerDTO model)
        {
            Owner ownerToUpdate = await this.GetByIdAsync<Owner>(id);

            if (ownerToUpdate == null)
            {
                //TODO exeption message
            }
            Type modelType = model.GetType();
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                var propertyValue = propertyInfo.GetValue(model);
                if (propertyValue != null)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    bool isPropertyTypeIEnumerable = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                    if (isPropertyTypeIEnumerable)
                    {
                        IEnumerable<Guid> petsId = propertyInfo.GetValue(model) as IEnumerable<Guid>;
                        await this.SavePetsToOwner(petsId, ownerToUpdate);

                        continue;
                    }
                    Type ownerToUpdateType = ownerToUpdate.GetType();
                    PropertyInfo propertyToUpdate = ownerToUpdateType.GetProperty(propertyInfo.Name);
                    propertyToUpdate.SetValue(ownerToUpdate, propertyValue);
                }
            }
            ownerToUpdate.UpdatedOn = DateTime.UtcNow;

            this.Dbcontext.Update(ownerToUpdate);
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


        private async Task SavePetsToOwner(IEnumerable<Guid> petsId, Owner owner)
        {
            foreach (Guid petId in petsId)
            {
                Pet pet = await petService.GetByIdAsync<Pet>(petId);
                if (pet == null)
                {
                    //TODO add error model pet does not exist
                    continue;
                }

                bool isPetAlreadyWithOwner = owner.Pets
                    .Any(opm => opm.OwnerId == owner.Id && opm.PetId == pet.Id);

                    if (isPetAlreadyWithOwner)
                {
                    //TODO add error model pet is with owner message
                    continue;
                }
                OwnerPetMapping ownerPetMapping = new OwnerPetMapping
                {
                    OwnerId = owner.Id,
                    PetId = pet.Id
                };

                await this.ownerPetMappingService.AddAsync<OwnerPetMapping>(ownerPetMapping);
            }
        }
        
    }
}
