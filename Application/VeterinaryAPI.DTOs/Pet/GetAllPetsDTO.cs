using System.Collections.Generic;

namespace VeterinaryAPI.DTOs.Pet
{
    public class GetAllPetsDTO
    {
        public IEnumerable<GetPetDTO> Pets { get; set; }
    }
}
