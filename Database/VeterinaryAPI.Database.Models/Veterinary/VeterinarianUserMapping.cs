using System;
using VeterinaryAPI.Database.Models.Users;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class VeterinarianUserMapping : BaseModel
    {

        public VeterinarianUserMapping()
            : base()
        {

        }

        public Guid VeterinarianId { get; set; }
        public virtual Veterinarian Veterinarian { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
