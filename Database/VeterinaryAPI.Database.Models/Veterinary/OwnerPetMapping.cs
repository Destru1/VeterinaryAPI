using System;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class OwnerPetMapping : BaseModel
    {
        public OwnerPetMapping()
            : base()
        {

        }

        public Guid OwnerId { get; set; }
        public virtual Owner Owner { get; set; }


        public Guid PetId { get; set; }
        public virtual Pet Pet { get; set; }

    }
}
