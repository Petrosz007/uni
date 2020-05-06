using System;
using ELTE.TravelAgency.Model;
using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("TravelAgency.Model")));

            // Dependency injection be�ll�t�sa az authentik�ci�hoz
            services.AddIdentity<Guest, IdentityRole<int>>()
                .AddEntityFrameworkStores<TravelAgencyContext>() // EF haszn�lata a TravelAgencyContext entit�s kontextussal
                .AddDefaultTokenProviders(); // Alap�rtelmezett token gener�tor haszn�lata (pl. SecurityStamp-hez)

            services.Configure<IdentityOptions>(options =>
            {
                // Jelsz� komplexit�s�ra vonatkoz� konfigur�ci�
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 3;

                // Hib�s bejelentkez�s eset�n az (ideiglenes) kiz�r�sra vonatkoz� konfigur�ci�
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // Felhaszn�l�kezel�sre vonatkoz� konfigur�ci�
                options.User.RequireUniqueEmail = true;
            });


            // Dependency injection be�ll�t�sa a Google konfigur�ci� kollekci�hoz
            services.Configure<GoogleConfig>(Configuration.GetSection("Google"));

            // Dependency injection be�ll�t�sa az utaz�ssal kapcsolatos szolg�ltat�shoz
            services.AddTransient<ITravelService, TravelService>();
            // Dependency injection be�ll�t�sa az alkalmaz�s �llapotra
            services.AddSingleton<ApplicationState>();
            // Dependency injection a IHttpContextAccessor-hoz
            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            // Munkamenetkezel�s be�ll�t�sa
            //services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15); // max. 15 percig �l a munkamenet
            });
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

            // Authentik�ci�s szolg�ltat�s haszn�lata
            app.UseAuthentication();

            app.UseAuthorization();

            // Munkamenetek haszn�lata
            app.UseSession();

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
