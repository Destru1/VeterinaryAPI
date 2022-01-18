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

            this.CreateMap<ICollection<OwnerPetMapping>, GetAllPetsDTO>()
                .ForMember(gapd => gapd.Pets, p => p.MapFrom(pets => pets));

            this.CreateMap<OwnerPetMapping, GetPetDTO>()
                .ForMember(gpd => gpd.Id, opm => opm.MapFrom(mapping => mapping.PetId))
                .ForMember(gpd => gpd.Name, opm => opm.MapFrom(mapping => mapping.Pet.Name))
                .ForMember(gpd => gpd.Type, opm => opm.MapFrom(mapping => mapping.Pet.Type))
                .ForMember(gpd => gpd.Breed, opm => opm.MapFrom(mapping => mapping.Pet.Breed))
                .ForMember(gpd => gpd.Age, opm => opm.MapFrom(mapping => mapping.Pet.Age));
            this.CreateMap<PostPetDTO, Pet>();
            this.CreateMap<PutPetDTO, Pet>();
        }
    }
}
