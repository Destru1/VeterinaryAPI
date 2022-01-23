using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VeterinaryAPI.Database;

namespace VeterinaryAPI.Infastructure.Extensions
{
    public static class DatabaseExtension
    {
        public async static Task MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<VeterinaryAPIDbcontext>();
                using (dbContext)
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
        }
    }
}
