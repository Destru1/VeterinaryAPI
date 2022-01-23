using System;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Users;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IUserRoleMappingService
    {
        Task<UserRoleMapping> AddRoleToUserAsync(Guid roleId, Guid userId);

        Task<bool> IsThereAnyDataInTableAsync();
    }
}
