using System;

namespace VeterinaryAPI.DTOs.Pet
{
    public class GetPetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }

        public string Breed { get; set; }

        public double Age { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
