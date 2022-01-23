using System;

namespace VeterinaryAPI.DTOs.VeterinarianPetMapping
{
    public class PatchVeterinarianPetDTO
    {
        public Guid VeterinarianId { get; set; }
        public Guid PetId { get; set; }
        public DateTime AppointmentDate { get; set; }

    }
}
