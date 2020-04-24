using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ELTE.TravelAgency.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ELTE.TravelAgency.Service
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
            // Dependency injection beállítása az adatbázis kontextushoz
            services.AddDbContext<TravelAgencyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Dependency injection beállítása az authentikációhoz
            services.AddIdentity<Guest, IdentityRole<int>>()
                .AddEntityFrameworkStores<TravelAgencyContext>() // EF használata a TravelAgencyContext entitás kontextussal
                .AddDefaultTokenProviders(); // Alapértelmezett token generátor használata (pl. SecurityStamp-hez)

            services.AddControllers();

            // Swagger generator regisztrálása
            services.AddSwaggerGen(c =>
            {
                // (név, OpenApiInfo) párok megadása szükséges
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Travel Agency API", Version = "v1"});

                // XML API dokumentáció felhasználása a Swaggerben
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Swagger használata (JSON végpontok)
            app.UseSwagger();

            // Swagger UI engedélyezése (böngészhetõ HTML végpontok)
            app.UseSwaggerUI(c =>
            {
                // a JSON végpont megadása (és engedélyezése szükséges)
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travel Agency API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
