using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;
using VeterinaryAPI.Database.Models.Veterinary;

namespace VeterinaryAPI.Database.Models.Users
{
    public class User : BaseModel
    {
        public User()
            : base()
        {
            this.Roles = new HashSet<UserRoleMapping>();
            this.Veterinarians = new HashSet<VeterinarianUserMapping>();
        }
        [Required]
        [StringLength(UserConstants.FIRST_NAME_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(UserConstants.LAST_NAME_MAX_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; }

        public virtual ICollection<UserRoleMapping> Roles { get; set; }
        public virtual ICollection<VeterinarianUserMapping> Veterinarians { get; set; }

    }
}
