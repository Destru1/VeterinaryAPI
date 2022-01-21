using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.DTOs.Roles;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {

        public RoleProfile()
        {
            this.CreateMap<Role, GetRoleIdDTO>();
        }
    }
}
