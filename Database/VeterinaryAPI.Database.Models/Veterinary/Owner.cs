using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class Owner : BaseModel
    {

        public Owner()
            :base()
        {
            this.Pets = new HashSet<OwnerPetMapping>();
        }

        public string FirstName { get; set; }

        public string  LastName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<OwnerPetMapping> Pets { get; set; }
    }
}
