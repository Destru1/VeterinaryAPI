using AutoMapper;
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
