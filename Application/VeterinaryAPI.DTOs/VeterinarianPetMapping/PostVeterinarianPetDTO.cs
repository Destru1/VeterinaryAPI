using System;

namespace VeterinaryAPI.DTOs.VeterinarianPetMapping
{
    public class PostVeterinarianPetDTO
    {
        public Guid VeterinarianId { get; set; }
        public Guid PetId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
