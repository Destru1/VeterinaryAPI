using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace VeterinaryAPI.Database.Seed.Seed.Interfaces
{
    public interface IBaseSeeder
    {
        Task SeedAsync(IServiceScope serviceProvider);
    }
}
