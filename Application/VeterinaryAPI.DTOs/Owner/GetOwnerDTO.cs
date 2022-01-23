using System;
using VeterinaryAPI.DTOs.Pet;

namespace VeterinaryAPI.DTOs.Owner
{
    public class GetOwnerDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public GetAllPetsDTO Pets { get; set; }
    }
}
