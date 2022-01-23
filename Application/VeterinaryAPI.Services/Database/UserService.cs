using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.DTOs.Roles;
using VeterinaryAPI.DTOs.User;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly ApplicationSettings options;
        private readonly IUserRoleMappingService userRoleMappingService;
        private readonly IRoleService roleService;
        public UserService(VeterinaryAPIDbcontext dbcontext,
            IMapper mapper,
            IOptions<ApplicationSettings> options,
            IUserRoleMappingService userRoleMappingService,
            IRoleService roleService)
            : base(dbcontext, mapper)
        {
            this.options = options.Value;
            this.userRoleMappingService = userRoleMappingService;
            this.roleService = roleService;
        }

        public async Task<T> RegisterAsync<T>(PostUserRegisterDTO model)
        {
            User user = await this.DbSet.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                throw new ArgumentException(ExceptionMessages.USER_EXIST_MESSAGE);
            }

            User userToCreate = this.Mapper.Map<User>(model);

            userToCreate.Salt = this.GeteratePasswordSalt();
            userToCreate.PasswordHash = this.HashPassword(model.Password, userToCreate.Salt);

            await this.DbSet.AddAsync(userToCreate);
            await this.Dbcontext.SaveChangesAsync();

            GetRoleIdDTO role = await this.roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.USER_ROLE_NAME);

            await this.userRoleMappingService.AddRoleToUserAsync(role.Id, userToCreate.Id);


            T userToReturn = this.Mapper.Map<T>(userToCreate);

            return userToReturn;
        }

        public async Task<string> LoginAsync(PostUserLoginDTO model)
        {
            User user = await this.GetUserByEmailAsync<User>(model.Email);

            string enteredHashedPassword = this.HashPassword(model.Password, user.Salt);

            if (user.PasswordHash != enteredHashedPassword)
            {
                throw new ArgumentException(ExceptionMessages.PASSWORD_DOES_NOT_MATCH);
            }

            string token = this.GenerateJwtToken(user.Id.ToString());

            return token;
        }


        public async Task<T> GetUserByIdAsync<T>(Guid id)
        {
            User user = await this.DbSet
                 .Include(u => u.Roles)
                 .ThenInclude(u => u.Role)
                 .SingleOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException(ExceptionMessages.USER_DOES_NOT_EXIST_MESSAGE);
            }

            T userToReturn = this.Mapper.Map<T>(user);
            return userToReturn;
        }

        public async Task<T> GetUserByEmailAsync<T>(string email)
        {
            User user = await this.DbSet.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                //throw new ArgumentException(ExceptionMessages.USER_DOES_NOT_EXIST_MESSAGE);
            }

            T userToReturn = this.Mapper.Map<T>(user);
            return userToReturn;
        }

        public string GeteratePasswordSalt()
        {

            byte[] saltArray = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltArray);
            }

            string salt = Encoding.UTF8.GetString(saltArray);

            return salt;
        }

        public string HashPassword(string password, string passwordSalt)
        {
            var salt = Encoding.ASCII.GetBytes(passwordSalt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

            return hashed;
        }

        public async Task<bool> IsThereAnyDataInTableAsync()
        {
            return await this.DbSet.AnyAsync();
        }



        private string GenerateJwtToken(string userId)
        {
            // generate token that is valid for 2 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.JwtApiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                                        new Claim("id", userId)
                                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials =
                            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }



    }
}
