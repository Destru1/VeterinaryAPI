using System;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.Pet;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IPetService
    {

        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<T> AddAsync<T>(PostPetDTO pet);

        Task<bool> UpdateAsync(Guid id, PutPetDTO pet);
        Task<bool> PartialUpdateAsync(Guid id, PatchPetDTO pet);
        Task<bool> DeleteAsync(Guid id);
    }
}
