using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.DTOs.Roles;
using VeterinaryAPI.DTOs.User;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Database.Seed.Seed
{
    public class UserRoleMappingSeeder : BaseSeeder
    {
        public override async Task SeedAsync(IServiceScope serviceProvider)
        {
            var userRoleMappingService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IUserRoleMappingService)) as IUserRoleMappingService;
            var userService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IUserService)) as IUserService;
            var roleService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IRoleService)) as IRoleService;

            if (await userRoleMappingService.IsThereAnyDataInTableAsync() == false)
            {
                var user = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.USER_EMAIL);
                var admin = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.ADMIN_EMAIL);

                var userRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.USER_ROLE_NAME);
                var adminRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.ADMIN_ROLE_NAME);

                await userRoleMappingService.AddRoleToUserAsync(userRole.Id, user.Id);
                await userRoleMappingService.AddRoleToUserAsync(adminRole.Id, admin.Id);
            }
        }
    }
}
