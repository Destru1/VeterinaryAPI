using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Common.Exeptions;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class OwnerPetMappingService : BaseService<OwnerPetMapping>, IOwnerPetMappingService
    {

        public OwnerPetMappingService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext, mapper)
        {

        }

        public async Task<T> GetByOwnerAndPetIdAsync<T>(Guid ownerId, Guid petId)
        {
            var ownerPetRelation = await this.DbSet
                .Where(opm => opm.OwnerId == ownerId && opm.PetId == petId)
                .Include(opm => opm.Owner)
                .Include(opm => opm.Pet)
                .SingleOrDefaultAsync();

            if (ownerPetRelation == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.OWNER_PET_MAPPING_DOES_NOT_EXIST_MESSAGE);
            }

            var ownerPetRelationToReturn = this.Mapper.Map<T>(ownerPetRelation);
            return ownerPetRelationToReturn;
        }

        public async Task<T> AddAsync<T>(OwnerPetMapping model)
        {
            OwnerPetMapping petToAdd = this.Mapper.Map<OwnerPetMapping>(model);

            await this.DbSet.AddAsync(petToAdd);
            await this.Dbcontext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(petToAdd);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid ownerId, Guid petId)
        {
            var ownerPetRelationToDelete = await this.GetByOwnerAndPetIdAsync<OwnerPetMapping>(ownerId, petId);

            this.DbSet.Remove(ownerPetRelationToDelete);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }


    }
}
