using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Common.Exeptions;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class VeterinarianService : BaseService<Veterinarian>, IVeterinarianService
    {
        private readonly IPositionService positionService;
        private readonly IVeterinarianPositionMappingService veterinarianPositionMappingService;
        public VeterinarianService(VeterinaryAPIDbcontext dbcontext,
            IMapper mapper,
            IPositionService positionService,
            IVeterinarianPositionMappingService veterinarianPositionMappingService)
            : base(dbcontext,mapper)
        {
            this.positionService = positionService;
            this.veterinarianPositionMappingService = veterinarianPositionMappingService;
        }


        public async Task<T> GetAllAsync<T>()
        {
            List<Veterinarian> veterinarians = await this.DbSet
                   .OrderBy(v => v.FirstName)
                   .ThenBy(v => v.LastName)
                   .Include(v => v.Positions)
                   .ThenInclude(v => v.Position)
                   .ToListAsync();

            T mappedVeterinarians = this.Mapper.Map<T>(veterinarians);

            return mappedVeterinarians;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Veterinarian veterinarian = await this.DbSet
                .Include(v => v.Positions)
                .ThenInclude(v => v.Position)
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
                throw new EntityDoesNotExistException(ExceptionMessages.VETERINARIAN_DOES_NOT_EXIST_MESSAGE);
            }

            Veterinarian updatedVeterinarian = this.Mapper.Map(veterinarian, veterinarianToUpdate);
            updatedVeterinarian.UpdatedOn = DateTime.UtcNow;

            this.Dbcontext.Update(updatedVeterinarian);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PartialUpdateAsync(Guid id, PatchVeterinarianDTO model)
        {
            Veterinarian veterinarianToUpdate = await this.GetByIdAsync<Veterinarian>(id);

            if (veterinarianToUpdate == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.VETERINARIAN_DOES_NOT_EXIST_MESSAGE);
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
                        IEnumerable<Guid> positionsId = propertyInfo.GetValue(model) as IEnumerable<Guid>;
                        await this.SavePositionToVeterinarian(positionsId, veterinarianToUpdate);
                        continue;
                    }
                    Type veterinarianToUpdateType = veterinarianToUpdate.GetType();
                    PropertyInfo propertyToUpdate = veterinarianToUpdateType.GetProperty(propertyInfo.Name);
                    propertyToUpdate.SetValue(veterinarianToUpdate, propertyValue);
                }
            }
            veterinarianToUpdate.UpdatedOn = DateTime.UtcNow;

            this.Dbcontext.Update(veterinarianToUpdate);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Veterinarian veterinarianToDelete = await this.GetByIdAsync<Veterinarian>(id);

            if (veterinarianToDelete == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.VETERINARIAN_DOES_NOT_EXIST_MESSAGE);
            }

            this.DbSet.Remove(veterinarianToDelete);

            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

      

        private async Task SavePositionToVeterinarian(IEnumerable<Guid> positionsId, Veterinarian veterinarian)
        {
            foreach (Guid positionId in positionsId)
            {
                Position position = await positionService.GetByIdAsync<Position>(positionId);
                if (position == null)
                {
                    this.AddModelError("PositionsId", string.Format(ExceptionMessages.POSITION_DOES_NOT_EXIST_MESSAGE, positionId));
               
                    continue;
                }
                bool isPositionAlreadyAssigned = veterinarian.Positions
                    .Any(vpm => vpm.VeterinarianId == veterinarian.Id && vpm.PositionId == position.Id);

                if (isPositionAlreadyAssigned)
                {
                    this.AddModelError("PositionsId", string.Format(ExceptionMessages.POSITION_ALREADY_ADDED_MESSAGE, positionId));
                    
                    continue;
                }

                VeterinarianPositionMapping veterinarianPositionMapping = new VeterinarianPositionMapping
                {
                    VeterinarianId = veterinarian.Id,
                    PositionId = position.Id
                };

                await this.veterinarianPositionMappingService.AddAsync<VeterinarianPositionMapping>(veterinarianPositionMapping);
            }
        }
    }
}
