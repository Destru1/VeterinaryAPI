using System.Threading.Tasks;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IRoleService
    {
        Task<T> GetRoleByNameAsync<T>(string name);

        Task<T> AddAsync<T>(string roleName);

        Task<bool> IsThereAnyDataInTableAsync();

    }
}
