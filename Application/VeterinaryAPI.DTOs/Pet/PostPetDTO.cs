using System;
using System.ComponentModel.DataAnnotations;
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
        [Range(PetConstants.MIN_AGE, PetConstants.MAX_AGE)]
        public double Age { get; set; }

    }
}
