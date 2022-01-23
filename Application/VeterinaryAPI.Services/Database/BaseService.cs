using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Models;

namespace VeterinaryAPI.Services.Database
{
    public abstract class BaseService<TEntity>
                            where TEntity : BaseModel
    {
        private readonly IActionContextAccessor actionContextAccessor;
        protected BaseService(VeterinaryAPIDbcontext dbcontext, IMapper mapper)
        {
            this.Dbcontext = dbcontext;
            this.DbSet = dbcontext.Set<TEntity>();
            this.Mapper = mapper;
        }

        protected BaseService(VeterinaryAPIDbcontext dbcontext, IMapper mapper, IActionContextAccessor actionContextAccessor)
            : this(dbcontext, mapper)
        {
            this.actionContextAccessor = actionContextAccessor;
        }

        protected IMapper Mapper { get; }
        protected VeterinaryAPIDbcontext Dbcontext { get; private set; }

        protected DbSet<TEntity> DbSet { get; private set; }

        protected void AddModelError(string errorKey, string errorMessage)
        {
            this.actionContextAccessor.ActionContext.ModelState.AddModelError(errorKey, errorMessage);
        }
    }
}
