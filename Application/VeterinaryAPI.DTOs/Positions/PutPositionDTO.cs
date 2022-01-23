using System.ComponentModel.DataAnnotations;
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
