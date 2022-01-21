using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI.Services.Database
{
   public class UserRoleMappingService : BaseService<UserRoleMapping>, IUserRoleMappingService
    {

        public UserRoleMappingService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
            : base(dbcontext, mapper)
        {

        }

        public async Task<UserRoleMapping> AddRoleToUserAsync(Guid roleId, Guid userId)
        {
            UserRoleMapping userRoleMapping = new UserRoleMapping()
            {
                RoleId = roleId,
                UserId = userId,
            };

            await this.DbSet.AddAsync(userRoleMapping);
            await this.Dbcontext.SaveChangesAsync();

            return userRoleMapping;
        }

        public async Task<bool> IsThereAnyDataInTableAsync()
        {
            return await this.DbSet.AnyAsync();
        }
    }
}
