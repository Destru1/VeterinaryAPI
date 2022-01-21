using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Seed.Seed.Interfaces
{
    public interface IBaseSeeder
    {
        Task SeedAsync(IServiceScope serviceProvider);
    }
}
