using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants;
using VeterinaryAPI.Common.Exeptions;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
    public class RoleService : BaseService<Role>, IRoleService
    {

        public RoleService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext, mapper)
        {

        }

        public async Task<T> GetRoleByNameAsync<T>(string name)
        {
            Role role = await this.DbSet.SingleOrDefaultAsync(r => r.Name == name);

            if (role == null)
            {
                throw new EntityDoesNotExistException(ExceptionMessages.ROLE_DOES_NOT_EXIST_MESSAGE);
            }

            T rolesToReturn = this.Mapper.Map<T>(role);

            return rolesToReturn;
        }

        public async Task<T> AddAsync<T>(string roleName)
        {
            Role role = new Role()
            {
                Name = roleName,
            };

            await this.DbSet.AddAsync(role);
            await this.Dbcontext.SaveChangesAsync();

            T roleToReturn = this.Mapper.Map<T>(role);

            return roleToReturn;
        }


        public async Task<bool> IsThereAnyDataInTableAsync()
        {
            return await this.DbSet.AnyAsync();
        }
    }
}
