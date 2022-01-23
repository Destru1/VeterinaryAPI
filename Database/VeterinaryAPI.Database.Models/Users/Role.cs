using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VeterinaryAPI.Common.Constants;

namespace VeterinaryAPI.Database.Models.Users
{
    public class Role : BaseModel
    {

        public Role()
            : base()
        {
            this.Users = new HashSet<UserRoleMapping>();
        }

        [Required]
        [StringLength(RoleConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public virtual ICollection<UserRoleMapping> Users { get; set; }
    }
}
