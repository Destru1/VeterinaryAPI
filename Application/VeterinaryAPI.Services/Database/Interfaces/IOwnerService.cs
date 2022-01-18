using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.Owner;

namespace VeterinaryAPI.Services.Database.Interfaces
{
   public interface IOwnerService
    {

        Task<T> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(Guid id);

        Task<T> AddAsync<T>(PostOwnerDTO owner);

        Task<bool> UpdateAsync(Guid id, PutOwnerDTO owner);

        Task<bool> PartialUpdateAsync(Guid id, PatchOwnerDTO model);
        Task<bool> DeleteAsync(Guid id);

    }
}
