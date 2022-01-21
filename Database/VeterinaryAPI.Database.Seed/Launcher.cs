using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryAPI.Database.Seed.Seed;
using VeterinaryAPI.Database.Seed.Seed.Interfaces;

namespace VeterinaryAPI.Database.Seed
{
    public static class Launcher
    {

        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            IEnumerable<IBaseSeeder> seeders = new List<IBaseSeeder>()
            {
                new RoleSeeder(),
                new UserSeeder(),
                new UserRoleMappingSeeder(),
            };

            using(var serviceProvider = app.ApplicationServices.CreateScope())
            {
                foreach (var seeder in seeders)
                {
                    await seeder.SeedAsync(serviceProvider);
                }
            }
        }
    }
}
