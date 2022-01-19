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
   public class VeterinarianPetMappingService : BaseService<VeterinarianPetMapping>, IVeterinarianPetMappingService
    {

        public VeterinarianPetMappingService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            :base(dbcontext,mapper)
        {

        }
        public async Task<T> GetModelByVeterinarianIdAndPetIdAsync<T>(Guid veterinarianId, Guid petId)
        {
            var model = await this.DbSet
                .Where(vpm => vpm.VeterinarianId == veterinarianId && vpm.PetId == petId)
                .OrderByDescending(vpm => vpm.CreatedOn)
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.VETERINARIAN_PET_MAPPING_DOES_NOT_EXIST_MESSAGE);
            }

            T modelToReturn = this.Mapper.Map<T>(model);
            return modelToReturn;
        }

        public async Task<T> CreateRelationAsync<T>(Guid veterinarianId, Guid petId, DateTime appointmentDate)
        {
            VeterinarianPetMapping model = new VeterinarianPetMapping()
            {
                VeterinarianId = veterinarianId,
                PetId = petId,
                AppointmentDate = appointmentDate,
            };
            await this.DbSet.AddAsync(model);
            await this.Dbcontext.SaveChangesAsync();

            T modelToReturn = this.Mapper.Map<T>(model);
            return modelToReturn;
        }

        public async Task<T> UpdateApointmentDateAsync<T>(Guid veterinarianId, Guid petId, DateTime appointmentDate)
        {
            VeterinarianPetMapping model =
                await this.GetModelByVeterinarianIdAndPetIdAsync<VeterinarianPetMapping>(veterinarianId, petId);

            model.AppointmentDate = appointmentDate;
            model.UpdatedOn = DateTime.UtcNow;

            this.DbSet.Update(model);
            await this.Dbcontext.SaveChangesAsync();

            T modelToReturn = this.Mapper.Map<T>(model);
            return modelToReturn;
        }

        public async Task<T> DeleteRalationAsync<T>(Guid veterinarianId, Guid petId)
        {
            VeterinarianPetMapping model = await this.GetModelByVeterinarianIdAndPetIdAsync<VeterinarianPetMapping>(veterinarianId, petId);

            this.DbSet.Remove(model);
            await this.Dbcontext.SaveChangesAsync();

            T modelToReturn = this.Mapper.Map<T>(model);
            return modelToReturn;
        }

       
     
    }
}
