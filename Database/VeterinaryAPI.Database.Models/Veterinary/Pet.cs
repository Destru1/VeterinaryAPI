﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Models.Veterinary
{
    public class Pet : BaseModel
    {

        public Pet()
            :base()
        {
            this.Veterinarians = new HashSet<VeterinarianPetMapping>();
            this.Owners = new HashSet<OwnerPetMapping>();

        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Breed { get; set; }

        public double Age { get; set; }

       
        public virtual ICollection<VeterinarianPetMapping> Veterinarians { get; set; }
        public virtual ICollection<OwnerPetMapping> Owners { get; set; }
    }
}
