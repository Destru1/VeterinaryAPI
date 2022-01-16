using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.DTOs.Pet
{
     public class PutPetDTO
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Breed { get; set; }

        public double Age { get; set; }

    }
}
