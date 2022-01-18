using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.Positions;

namespace VeterinaryAPI.Services.Database.Interfaces
{
        public interface IPositionService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<T> AddAsync<T>(PostPositionDTO model);

        Task<bool> UpdateAsync(Guid id, PutPositionDTO model);
        
        Task<bool> DeleteAsync(Guid id);
    }
}
