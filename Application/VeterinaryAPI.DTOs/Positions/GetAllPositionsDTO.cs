using System.Collections.Generic;

namespace VeterinaryAPI.DTOs.Positions
{
    public class GetAllPositionsDTO
    {
        public IEnumerable<GetPositionDTO> Positions { get; set; }

        public int PositionsCount { get; set; }
    }
}
