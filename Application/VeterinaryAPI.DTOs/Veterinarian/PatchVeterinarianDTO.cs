using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.DTOs.Veterinarian
{
    public class PatchVeterinarianDTO
    {
        [StringLength(VeterinarianConstants.NAMES_MAX_LENGTH)]
        public string FirstName { get; set; }

        [StringLength(VeterinarianConstants.NAMES_MAX_LENGTH)]
        public string LastName { get; set; }

        [StringLength(VeterinarianConstants.PHONENUMBER_MAX_LENGTH)]
        public string PhoneNumber { get; set; }

        [StringLength(VeterinarianConstants.EMAIL_MAX_LENGTH)]
        public string Email { get; set; }

        public IEnumerable<Guid> PositionsId { get; set; }
    }
}
