using System.ComponentModel.DataAnnotations;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.DTOs.Owner
{
    public class PutOwnerDTO
    {
        [Required]
        [StringLength(OwnerConstants.NAMES_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(OwnerConstants.NAMES_MAX_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [StringLength(OwnerConstants.PHONENUMBER_MAX_LENGHT)]
        public string PhoneNumber { get; set; }
    }
}
