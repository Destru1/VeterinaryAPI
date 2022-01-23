using System;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IOwnerPetMappingService
    {
        Task<T> GetByOwnerAndPetIdAsync<T>(Guid ownerId, Guid petId);

        Task<T> AddAsync<T>(OwnerPetMapping model);

        Task<bool> DeleteAsync(Guid ownerId, Guid petId);
    }
}
