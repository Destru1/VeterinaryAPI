using System;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class VeterinarianPetMapping : BaseModel
    {
        public VeterinarianPetMapping()
            : base()
        {

        }

        public Guid VeterinarianId { get; set; }
        public virtual Veterinarian Veterinarian { get; set; }


        public Guid PetId { get; set; }
        public virtual Pet Pet { get; set; }


        public DateTime AppointmentDate { get; set; }


    }
}
