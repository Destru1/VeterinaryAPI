using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.Veterinarian
{
   public class GetAllVeterinariansDTO
    {
        public ICollection<GetVeterinarianDTO> Veterinarians { get; set; }

    }
}
