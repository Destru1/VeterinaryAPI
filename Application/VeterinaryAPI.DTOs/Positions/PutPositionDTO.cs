﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryAPI.Common.Constants.ModelConstants;

namespace VeterinaryAPI.DTOs.Positions
{
    public class PutPositionDTO
    {

        [Required]
        [StringLength(PositionConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }
    }
}
