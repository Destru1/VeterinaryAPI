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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VeterinaryAPI.Common;
using VeterinaryAPI.Database;
using VeterinaryAPI.Database.Seed;
using VeterinaryAPI.Infastructure.Extensions;
using VeterinaryAPI.Infastructure.Filters;
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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = $"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.OperationFilter<AuthResponsesOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

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


                app.MigrateDatabase().GetAwaiter().GetResult();
                app.SeedDatabaseAsync().GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();


            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleMappingService, UserRoleMappingService>();
        }
    }
}


