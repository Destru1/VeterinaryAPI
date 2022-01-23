using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Database.Seed.Seed
{
    public class RoleSeeder : BaseSeeder
    {
        public override async Task SeedAsync(IServiceScope serviceProvider)
        {
            IRoleService roleService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IRoleService)) as IRoleService;

            if (await roleService.IsThereAnyDataInTableAsync() == false)
            {
                await roleService.AddAsync<Role>(GlobalConstants.USER_ROLE_NAME);
                await roleService.AddAsync<Role>(GlobalConstants.ADMIN_ROLE_NAME);
            }
        }
    }
}
