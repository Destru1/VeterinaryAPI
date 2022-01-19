﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.DTOs.Veterinarian
{
  public  class PostVeterinarianDTO
    {
        [Required]
        [StringLength(VeterinarianConstants.NAMES_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(VeterinarianConstants.NAMES_MAX_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [StringLength(VeterinarianConstants.PHONENUMBER_MAX_LENGTH)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(VeterinarianConstants.EMAIL_MAX_LENGTH)]
        public string Email { get; set; }
    }
}
