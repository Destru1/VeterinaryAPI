using System;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IVeterinarianPositionMappingService
    {
        Task<T> GetByVeterinarianAndPositionIdAsync<T>(Guid veterinarianId, Guid positionId);
        Task<T> AddAsync<T>(VeterinarianPositionMapping model);
        Task<bool> DeleteAsync(Guid veterinarianId, Guid positionId);
    }
}
