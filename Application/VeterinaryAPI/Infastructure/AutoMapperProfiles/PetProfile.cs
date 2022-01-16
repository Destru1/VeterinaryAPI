using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Pet;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class PetProfile : Profile
    {

        public PetProfile()
        {
            this.CreateMap<Pet, GetPetDTO>();

            this.CreateMap<IEnumerable<Pet>, GetAllPetsDTO>()
                .ForMember(gap => gap.Pets, p => p.MapFrom(pets => pets));

            this.CreateMap<PostPetDTO, Pet>();
            this.CreateMap<PutPetDTO, Pet>();
        }
    }
}
