using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.Database.Models.Veterinary
{
   public class Position : BaseModel
    {
        public Position()
            :base()
        {
            this.Veterinarians = new HashSet<VeterinarianPositionMapping>();
        }

        [Required]
        [StringLength(PositionConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public virtual ICollection<VeterinarianPositionMapping> Veterinarians { get; set; }
    }
}
