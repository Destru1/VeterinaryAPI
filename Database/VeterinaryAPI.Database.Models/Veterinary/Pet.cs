using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class Pet : BaseModel
    {

        public Pet()
            :base()
        {
            this.Veterinarians = new HashSet<VeterinarianPetMapping>();
            this.Owners = new HashSet<OwnerPetMapping>();

        }
        [Required]
        [StringLength(PetConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(PetConstants.TYPE_MAX_LENGTH)]
        public string Type { get; set; }

        [Required]
        [StringLength(PetConstants.BREED_MAX_LENGTH)]
        public string Breed { get; set; }

        [Required]
        [Range(PetConstants.MIN_AGE, PetConstants.MAX_AGE)]
        public double Age { get; set; }

       
        public virtual ICollection<VeterinarianPetMapping> Veterinarians { get; set; }
        public virtual ICollection<OwnerPetMapping> Owners { get; set; }
    }
}
