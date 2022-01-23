using System.Collections.Generic;

namespace VeterinaryAPI.DTOs.Owner
{
    public class GetAllOwnersDTO
    {
        public ICollection<GetOwnerDTO> Owners { get; set; }
    }
}
