using AutoMapper;
using System.Collections.Generic;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Veterinarian;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class VeterinarianProfile : Profile
    {
        public VeterinarianProfile()
        {

            this.CreateMap<IEnumerable<Veterinarian>, GetAllVeterinariansDTO>()
                .ForMember(gav => gav.Veterinarians, t => t.MapFrom(veterinarians => veterinarians));

            this.CreateMap<Veterinarian, GetVeterinarianDTO>()
             .ForMember(gvd => gvd.Positions, p => p.MapFrom(position => position.Positions));

            this.CreateMap<PostVeterinarianDTO, Veterinarian>();
            this.CreateMap<PutVeterinarianDTO, Veterinarian>();
        }
    }
}
