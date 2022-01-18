using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Positions;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class PositionService : BaseService<Position>, IPositionService
    {

        public PositionService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext,mapper)
        {

        }


        public async Task<T> GetAllAsync<T>()
        {
            List<Position> positions = await this.DbSet
                .OrderBy(p => p.Name)
                .ToListAsync();

            T mappedPositions = this.Mapper.Map<T>(positions);
            return mappedPositions;
        }
     

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            Position position = await this.DbSet.SingleOrDefaultAsync(p => p.Id == id);

            T mappedPosition = this.Mapper.Map<T>(position);

            return mappedPosition;
        }

        public async Task<T> AddAsync<T>(PostPositionDTO model)
        {
            Position positionToAdd = this.Mapper.Map<Position>(model);

            await this.DbSet.AddAsync(positionToAdd);
            await this.Dbcontext.SaveChangesAsync();

            T result = this.Mapper.Map<T>(positionToAdd);

            return result;
        }

        public async Task<bool> UpdateAsync(Guid id, PutPositionDTO model)
        {
            Position positionToUpdate = await this.GetByIdAsync<Position>(id);

            if (positionToUpdate == null)
            {
                throw new Exception("Position does not exist");
            }

            Position updatedPosition = this.Mapper.Map(model, positionToUpdate);
            updatedPosition.UpdatedOn = DateTime.UtcNow;

            this.DbSet.Update(updatedPosition);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Position positionToDelete = await this.GetByIdAsync<Position>(id);

            if (positionToDelete == null)
            {
                return false;
            }

            this.DbSet.Remove(positionToDelete);
            await this.Dbcontext.SaveChangesAsync();

            return true;
        }

    }
}
