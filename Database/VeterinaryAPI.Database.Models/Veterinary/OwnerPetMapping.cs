using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Models.Veterinary
{
     public class OwnerPetMapping : BaseModel
    {
        public OwnerPetMapping()
            :base()
        {

        }

        public Guid OwnerId { get; set; }
        public virtual Owner Owner { get; set; }


        public Guid PetId { get; set; }
        public virtual Pet Pet { get; set; }

    }
}
