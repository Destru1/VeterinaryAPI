using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.DTOs.Pet
{
     public class PostPetDTO
    {
        [Required]
        [StringLength(PetConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [StringLength(PetConstants.TYPE_MAX_LENGTH)]
        public string Type { get; set; }

        [Required]
        [StringLength(PetConstants.BREED_MAX_LENGTH)]
        public string Breed { get; set; }

        [Required]
        [StringLength(PetConstants.MAX_AGE)]
        public double Age { get; set; }

    }
}
