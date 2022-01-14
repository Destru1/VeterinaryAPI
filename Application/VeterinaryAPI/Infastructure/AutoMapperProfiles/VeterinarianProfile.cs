using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class VeterinarianProfile : Profile
    {
        public VeterinarianProfile()
        {
            this.CreateMap<Veterinarian, GetVeterinarianDTO>();

            this.CreateMap<IEnumerable<Veterinarian>, GetAllVeterinariansDTO>()
                .ForMember(gav => gav.Veterinarians, t => t.MapFrom(veterinarians => veterinarians));

            this.CreateMap<PostVeterinarianDTO, Veterinarian>();
            this.CreateMap<PutVeterinarianDTO, Veterinarian>();
        }
    }
}
