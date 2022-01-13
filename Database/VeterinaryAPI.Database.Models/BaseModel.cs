using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryAPI.Database.Models
{
    public abstract class BaseModel
    {

        public BaseModel()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = DateTime.UtcNow;
            this.UpdatedOn = null;
        }


        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
