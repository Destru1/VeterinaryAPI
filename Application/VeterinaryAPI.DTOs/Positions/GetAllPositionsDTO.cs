using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.Positions
{
    public class GetAllPositionsDTO
    {
        public IEnumerable<GetPositionDTO> Positions { get; set; }

        public int PositionsCount { get; set; }
    }
}
