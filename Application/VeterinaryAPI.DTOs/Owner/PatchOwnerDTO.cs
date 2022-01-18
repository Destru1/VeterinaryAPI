using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.DTOs.Owner
{
    public class PatchOwnerDTO
    {
        [StringLength(OwnerConstants.NAMES_MAX_LENGTH)]
        public string FirstName { get; set; }

        [StringLength(OwnerConstants.NAMES_MAX_LENGTH)]
        public string LastName { get; set; }

        [StringLength(OwnerConstants.PHONENUMBER_MAX_LENGHT)]
        public string PhoneNumber { get; set; }

        public IEnumerable<Guid> PetsId { get; set; }
    }
}
