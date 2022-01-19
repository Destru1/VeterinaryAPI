using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VeterinaryAPI.Database;
using VeterinaryAPI.Infastructure.Middleware;
using VeterinaryAPI.Services.Database;
using VeterinaryAPI.Services.Database.Interfaces;

namespace VeterinaryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VeterinaryAPI", Version = "v1" });
            });

            services.AddDbContext<VeterinaryAPIDbcontext>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            RegisterDatabaseServices(services);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VeterinaryAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterDatabaseServices(IServiceCollection services)
        {
            services.AddScoped<IVeterinarianService, VeterinarianService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IOwnerPetMappingService, OwnerPetMappingService>();
            services.AddScoped<IVeterinarianPetMappingService, VeterinarianPetMappingService>();
            services.AddScoped<IVeterinarianPositionMappingService, VeterinarianPositionService>();
        }
    }
}


