using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Common.Exeptions;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class VeterinarianPositionService : BaseService<VeterinarianPositionMapping>, IVeterinarianPositionMappingService
    {

        public VeterinarianPositionService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            :base(dbcontext,mapper)
        {

        }

        public async Task<T> GetByVeterinarianAndPositionIdAsync<T>(Guid veterinarianId, Guid positionId)
        {
            var veterinarianPositionRelation = await this.DbSet
                .Where(vpm => vpm.VeterinarianId == veterinarianId && vpm.PositionId == positionId)
                .Include(vpm => vpm.Veterinarian)
                .Include(vpm => vpm.Position)
                .SingleOrDefaultAsync();

            if (veterinarianPositionRelation == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.VETERINARIAN_POSITION_MAPPING_DOES_NOT_EXIST_MESSAGE);
             
            }

            var veterinarianRelationToReturn = this.Mapper.Map<T>(veterinarianPositionRelation);
            return veterinarianRelationToReturn;
        }

        public async Task<T> AddAsync<T>(VeterinarianPositionMapping model)
        {
            VeterinarianPositionMapping positionToAdd = this.Mapper.Map<VeterinarianPositionMapping>(model);

            await this.DbSet.AddAsync(positionToAdd);
            await this.Dbcontext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(positionToAdd);

            return result;
        }

        public async Task<bool> DeleteAsync(Guid veterinarianId, Guid positionId)
        {
            var veterinarianPositionToDelete = await this.GetByVeterinarianAndPositionIdAsync<VeterinarianPositionMapping>(veterinarianId, positionId);

            this.DbSet.Remove(veterinarianPositionToDelete);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }
    }
}
