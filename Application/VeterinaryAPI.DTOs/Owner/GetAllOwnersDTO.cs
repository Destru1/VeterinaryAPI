using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.Owner
{
    public class GetAllOwnersDTO
    {
        public ICollection<GetOwnerDTO> Owners { get; set; }
    }
}
