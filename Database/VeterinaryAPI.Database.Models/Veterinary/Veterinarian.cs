using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Models.Veterinary
{
     public class Veterinarian : BaseModel
    {
        public Veterinarian()
            :base()
        {
            this.Pets = new HashSet<VeterinarianPetMapping>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual ICollection<VeterinarianPetMapping> Pets { get; set; }
    }
}
