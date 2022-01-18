using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.Positions
{
    public class GetPositionDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
