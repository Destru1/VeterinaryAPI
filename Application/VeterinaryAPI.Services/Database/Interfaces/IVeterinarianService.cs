using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IVeterinarianService
    {
        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<Veterinarian> AddAsync(PostVeterinarianDTO veterinarian);

        Task<bool> UpdateAsync(Guid id, PutVeterinarianDTO veterinarian);

        Task<bool> PartialUpdateAsync(Guid id, PatchVeterinarianDTO model);

        Task<bool> DeleteAsync(Guid id);

    }
}
