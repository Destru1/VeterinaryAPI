using System;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class VeterinarianPositionMapping : BaseModel
    {
        public VeterinarianPositionMapping()
            : base()
        {

        }
        public Guid VeterinarianId { get; set; }
        public virtual Veterinarian Veterinarian { get; set; }


        public Guid PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
