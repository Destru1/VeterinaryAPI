using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.DTOs.User;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Database.Seed.Seed
{
    public class UserSeeder : BaseSeeder
    {
        public override async Task SeedAsync(IServiceScope serviceProvider)
        {
            var userService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IUserService)) as IUserService;

            if (await userService.IsThereAnyDataInTableAsync() == false)
            {
                PostUserRegisterDTO user = new PostUserRegisterDTO()
                {
                    Email = GlobalConstants.USER_EMAIL,
                    FirstName = GlobalConstants.USER_FIRST_NAME,
                    LastName = GlobalConstants.USER_LAST_NAME,
                    Password = GlobalConstants.USER_PASSWORD,
                    RepeatPassword = GlobalConstants.USER_PASSWORD,
                };

                PostUserRegisterDTO admin = new PostUserRegisterDTO()
                {
                    Email = GlobalConstants.ADMIN_EMAIL,
                    FirstName = GlobalConstants.ADMIN_FIRST_NAME,
                    LastName = GlobalConstants.ADMIN_LAST_NAME,
                    Password = GlobalConstants.ADMIN_PASSWORD,
                    RepeatPassword = GlobalConstants.ADMIN_PASSWORD,
                };

                await userService.RegisterAsync<User>(user);
                await userService.RegisterAsync<User>(admin);
            }
        }
    }
}
