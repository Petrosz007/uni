using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ELTE.TravelAgency.Admin.Persistence;
using ELTE.TravelAgency.Data;
using ELTE.TravelAgency.Service.Controllers;
using ELTE.TravelAgency.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace ELTE.TravelAgency.Service.Test
{
	public class TravelAgencyIntegrationTest : IDisposable
	{
	    public static IList<City> CityData = new List<City>
	    {
	        new City { Id = 1, Name = "TESTCITY" }
	    };

	    public static IList<Building> BuildingData = new List<Building>
	    {
			new Building
            {
                Id = 1,
                CityId = CityData[0].Id,
                City = CityData[0],
                Name = "TESTBUILDING1",
                SeaDistance = 1,
                Shore = ShoreType.Rocky,
                Features = Feature.CoastService | Feature.MainRoad
            },
            new Building
            {
                Id = 2,
                CityId = CityData[0].Id,
                City = CityData[0],
                Name = "TESTBUILDING2",
                SeaDistance = 10,
                Shore = ShoreType.Gravelly,
                Features = Feature.None
            },
            new Building
            {
                Id = 3,
                CityId = CityData[0].Id,
                City = CityData[0],
                Name = "TESTBUILDING3",
                SeaDistance = 100,
                Shore = ShoreType.Sandy,
                Features = Feature.PrivateParking
            }
		};

		private readonly List<CityDTO> _cityDTOs;
		private readonly List<BuildingDTO> _buildingDTOs;
	    private readonly ITravelAgencyPersistence _persistence;

        private readonly IHost _server;
        private readonly HttpClient _client;

        public TravelAgencyIntegrationTest()
        {
            _cityDTOs = CityData.Select(city => new CityDTO
            {
                Id = city.Id,
                Name = city.Name
            }).ToList();

            _buildingDTOs = BuildingData.Select(building => new BuildingDTO
            {
                Id = building.Id,
                Name = building.Name,
                City = _cityDTOs.Single(city => city.Id == building.CityId),
                Comment = building.Comment,
                Images = new List<ImageDTO>(),
                LocationX = building.LocationX,
                LocationY = building.LocationY,
                SeaDistance = building.SeaDistance,
                Shore = building.Shore,
                Features = FeatureDTO.Convert(building.Features)
            }).ToList();

            var hostBuilder = new HostBuilder() // szerver konfiguráció összeállítása
                .ConfigureWebHost(webHost =>
                {
                    webHost
                        .UseTestServer() // test szerver implementáció használata
                        .UseStartup<TestStartup>()
                        .UseEnvironment("Development");
                });
            
            _server = hostBuilder.Start(); // szerver példányosítása és elindítása

            _client = _server.GetTestClient(); // kliens példányosítása
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _persistence = new TravelAgencyServicePersistence(_client); // kliens oldali perzisztencia réteg példányosítása
        }

        public void Dispose()
        {
            var dbContext = _server.Services.GetRequiredService<TravelAgencyContext>();
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
		public async void GetCityTest()
        {
            IEnumerable<CityDTO> result = await _persistence.ReadCitiesAsync();

			// Assert
			Assert.Equal(_cityDTOs, result);
		}

		[Fact]
		public async void GetBuildingTest()
        {
            IEnumerable<BuildingDTO> result = await _persistence.ReadBuildingsAsync();

            // Assert
			Assert.Equal(_buildingDTOs, result);
		}
    }
}
