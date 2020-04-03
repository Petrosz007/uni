using System;
using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ELTE.TravelAgency.Web
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
            // Dependency injection be�ll�t�sa az adatb�zis kontextushoz
            services.AddDbContext<TravelAgencyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Dependency injection be�ll�t�sa a Google konfigur�ci� kollekci�hoz
            services.Configure<GoogleConfig>(Configuration.GetSection("Google"));

            // Dependency injection be�ll�t�sa az utaz�ssal kapcsolatos szolg�ltat�shoz
            services.AddTransient<ITravelService, TravelService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Adatb�zis inicializ�l�sa
            DbInitializer.Initialize(serviceProvider, Configuration.GetValue<string>("ImageStore"));
        }
    }
}
