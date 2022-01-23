using System;

namespace VeterinaryAPI.DTOs.VeterinarianPetMapping
{
    public class GetVeterinarianPetDTO
    {
        public Guid Id { get; set; }

        public Guid VeterinarianId { get; set; }

        public string VeterinarianName { get; set; }

        public Guid PetId { get; set; }

        public string PetName { get; set; }
        public string PetType { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}
