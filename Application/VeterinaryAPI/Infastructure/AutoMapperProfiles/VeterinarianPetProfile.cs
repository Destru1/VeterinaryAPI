using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.VeterinarianPetMapping;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class VeterinarianPetProfile : Profile
    {

        public VeterinarianPetProfile()
        {
            this.CreateMap<VeterinarianPetMapping, GetVeterinarianPetDTO>()
                .ForMember(gvpd => gvpd.VeterinarianName, vpm => vpm.MapFrom(mapping => mapping.Veterinarian.LastName));
        }
    }
}
