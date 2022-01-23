using System;
using VeterinaryAPI.DTOs.Positions;

namespace VeterinaryAPI.DTOs.Veterinarian
{
    public class GetVeterinarianDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public GetAllPositionsDTO Positions { get; set; }


    }
}
