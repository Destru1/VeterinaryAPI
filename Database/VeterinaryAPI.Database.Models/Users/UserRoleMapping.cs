using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Models.Users
{
     public class UserRoleMapping : BaseModel
    {
        public UserRoleMapping()
            : base()
        {

        }

        [Required]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
