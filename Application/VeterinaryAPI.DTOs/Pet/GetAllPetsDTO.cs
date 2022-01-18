using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.Pet
{
     public class GetAllPetsDTO
    {
        public IEnumerable<GetPetDTO> Pets { get; set; }
    }
}
