using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class Veterinarian : BaseModel
    {
        public Veterinarian()
            : base()
        {
            this.Pets = new HashSet<VeterinarianPetMapping>();
            this.Positions = new HashSet<VeterinarianPositionMapping>();
            this.Users = new HashSet<VeterinarianUserMapping>();
        }

        [Required]
        [StringLength(VeterinarianConstants.NAMES_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(VeterinarianConstants.NAMES_MAX_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [StringLength(VeterinarianConstants.PHONENUMBER_MAX_LENGTH)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(VeterinarianConstants.EMAIL_MAX_LENGTH)]
        public string Email { get; set; }

        public virtual ICollection<VeterinarianPetMapping> Pets { get; set; }
        public virtual ICollection<VeterinarianPositionMapping> Positions { get; set; }
        public virtual ICollection<VeterinarianUserMapping> Users { get; set; }
    }
}
