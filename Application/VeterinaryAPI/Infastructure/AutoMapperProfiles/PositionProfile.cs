using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Models.Veterinary;
using VeterinaryAPI.DTOs.Positions;

namespace VeterinaryAPI.Infastructure.AutoMapperProfiles
{
    public class PositionProfile : Profile
    {


        public PositionProfile()
        {
            this.CreateMap<PostPositionDTO, Position>();
            this.CreateMap<Position, GetPositionDTO>();
            this.CreateMap<ICollection<Position>, GetAllPositionsDTO>()
                .ForMember(gapd => gapd.Positions, p => p.MapFrom(positions => positions))
                .ForMember(gapd => gapd.PositionsCount, p => p.MapFrom(positions => positions.Count));
            this.CreateMap<PutPositionDTO, Position>();
                
        }
    }
}
