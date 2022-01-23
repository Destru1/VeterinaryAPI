using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class Owner : BaseModel
    {

        public Owner()
            : base()
        {
            this.Pets = new HashSet<OwnerPetMapping>();
        }
        [Required]
        [StringLength(OwnerConstants.NAMES_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(OwnerConstants.NAMES_MAX_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [StringLength(OwnerConstants.PHONENUMBER_MAX_LENGHT)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<OwnerPetMapping> Pets { get; set; }
    }
}
