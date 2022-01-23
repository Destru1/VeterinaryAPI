using System;
using System.ComponentModel.DataAnnotations;

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
