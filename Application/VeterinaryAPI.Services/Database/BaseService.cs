using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models;

namespace VeterinaryAPI.Services.Database
{
   public abstract class BaseService<TEntity> 
                            where TEntity : BaseModel
    {

        protected BaseService(VeterinaryAPIDbcontext dbcontext)
        {
            this.Dbcontext = dbcontext;
            this.DbSet = dbcontext.Set<TEntity>();
        }


        protected VeterinaryAPIDbcontext Dbcontext { get; private set; }

        protected DbSet<TEntity> DbSet { get; private set; }
    }
}
