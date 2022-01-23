using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.User;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Controllers
{
    public class UserController : BaseAPIController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(PostUserLoginDTO model)
        {

            string token = await this.userService.LoginAsync(model);

            GetTokenDTO tokenObject = new GetTokenDTO(token);

            return this.Ok(tokenObject);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(PostUserRegisterDTO model)
        {
            GetUserInformationDTO user = await this.userService.RegisterAsync<GetUserInformationDTO>(model);

            return this.Ok(user);
        }
    }
}
