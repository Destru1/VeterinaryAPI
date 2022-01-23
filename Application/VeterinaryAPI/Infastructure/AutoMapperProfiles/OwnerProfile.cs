using AutoMapper;
using System.Collections.Generic;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Owner;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class OwnerProfile : Profile
    {

        public OwnerProfile()
        {
            this.CreateMap<IEnumerable<Owner>, GetAllOwnersDTO>()
                .ForMember(gao => gao.Owners, o => o.MapFrom(owners => owners));
            this.CreateMap<Owner, GetOwnerDTO>()
                .ForMember(god => god.Pets, p => p.MapFrom(pet => pet.Pets));
            this.CreateMap<PostOwnerDTO, Owner>();
            this.CreateMap<PutOwnerDTO, Owner>();
        }
    }
}
