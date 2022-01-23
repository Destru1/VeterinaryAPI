using System.Collections.Generic;

namespace VeterinaryAPI.DTOs.Veterinarian
{
    public class GetAllVeterinariansDTO
    {
        public ICollection<GetVeterinarianDTO> Veterinarians { get; set; }

    }
}
