using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Users;
using VeterinaryAPI.DTOs.Roles;
using VeterinaryAPI.DTOs.User;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
     
            this.CreateMap<User, GetUserForSessionDTO>()
                .ForMember(gufsd => gufsd.Roles, x => x.MapFrom(u => u.Roles));

            this.CreateMap<UserRoleMapping, GetRolesForSessionDTO>()
                .ForMember(grfsd => grfsd.RoleName, x => x.MapFrom(urm => urm.Role.Name));

            this.CreateMap<User, GetUserInformationDTO>();

            this.CreateMap<User, GetUserIdDTO>();

            this.CreateMap<PostUserLoginDTO, User>();

            this.CreateMap<PostUserRegisterDTO, User>();
        }
    }
}
