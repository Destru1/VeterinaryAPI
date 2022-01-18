using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.VeterinarianPetMapping
{
    public class PostVeterinarianPetDTO
    {
        public Guid VeterinarianId { get; set; }
        public Guid PetId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
