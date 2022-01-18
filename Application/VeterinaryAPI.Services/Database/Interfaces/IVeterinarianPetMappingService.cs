using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IVeterinarianPetMappingService
    {
        Task<T> GetModelByVeterinarianIdAndPetIdAsync<T>(Guid veterinarianId, Guid petId);

        Task<T> CreateRelationAsync<T>(Guid veterinarianId, Guid petId, DateTime appointmentDate);
        Task<T> UpdateApointmentDateAsync<T>(Guid veterinarianId, Guid petId, DateTime appointmentDate);
        Task<T> DeleteRalationAsync<T>(Guid veterinarianId, Guid petId);
    }
}
