using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ELTE.TravelAgency.Model;
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

namespace ELTE.TravelAgency.Service.Test
{
	public class TestStartup
	{
		public TestStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// Dependency injection beállítása az adatbázis kontextushoz
			services.AddDbContext<TravelAgencyContext>(options =>
		        options.UseInMemoryDatabase("TravelAgencyTest"));

            services.AddControllers();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

            app.UseRouting();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

			// adatok inicializációja
			var dbContext = serviceProvider.GetRequiredService<TravelAgencyContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Cities.AddRange(TravelAgencyIntegrationTest.CityData);
		    dbContext.Buildings.AddRange(TravelAgencyIntegrationTest.BuildingData);
		    dbContext.SaveChanges();
        }
	}
}
