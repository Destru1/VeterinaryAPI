using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Users;
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

            if (token == null)
            {
                throw new ArgumentException();
            }
            return this.Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(PostUserRegisterDTO model)
        {
            User user = await this.userService.RegisterAsync<User>(model);

            if (user == null)
            {
                throw new ArgumentException();
            }
            return this.Ok(user);
        }
    }
}
