using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class Pet : BaseModel
    {

        public Pet()
            :base()
        {
            this.Veterinarians = new HashSet<VeterinarianPetMapping>();
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Breed { get; set; }

        public double Age { get; set; }

       


        public Guid OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual ICollection<VeterinarianPetMapping> Veterinarians { get; set; }
    }
}
