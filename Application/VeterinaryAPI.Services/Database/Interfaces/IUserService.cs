using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.DTOs.User;

namespace VeterinaryAPI.Services.Database.Interfaces
{
    public interface IUserService
    {
        Task<T> RegisterAsync<T>(PostUserRegisterDTO model);

        Task<string> LoginAsync(PostUserLoginDTO model);

        Task<T> GetUserByIdAsync<T>(Guid id);

        Task<T> GetUserByEmailAsync<T>(string email);

        string GeteratePasswordSalt();

        string HashPassword(string password, string passwordSalt);

        Task<bool> IsThereAnyDataInTableAsync();



    }
}
