using System;
using System.Collections.Generic;
using System.Linq;
using ELTE.TravelAgency.Data;
using ELTE.TravelAgency.Service.Controllers;
using ELTE.TravelAgency.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ELTE.TravelAgency.Service.Test
{
	public class TravelAgencyMockTest
	{
		private readonly List<CityDTO> _cityDTOs;
		private readonly List<BuildingDTO> _buildingDTOs;

	    private Mock<DbSet<City>> _cityMock;
	    private Mock<DbSet<Building>> _buildingMock;
	    private Mock<TravelAgencyContext> _entityMock;

        public TravelAgencyMockTest()
		{
			// adatok inicializációja
			var cityData = new List<City>
			{
				new City { Id = 1, Name = "TESTCITY" }
			};

            var buildingData = new List<Building>
			{
                new Building
                {
                    Id = 1,
                    CityId = cityData[0].Id,
                    City = cityData[0],
                    Name = "TESTBUILDING1",
                    SeaDistance = 1,
                    Shore = ShoreType.Rocky,
                    Features = Feature.CoastService | Feature.MainRoad
                },
                new Building
                {
                    Id = 2,
                    CityId = cityData[0].Id,
                    City = cityData[0],
                    Name = "TESTBUILDING2",
                    SeaDistance = 10,
                    Shore = ShoreType.Gravelly,
                    Features = Feature.None
                },
                new Building
                {
                    Id = 3,
                    CityId = cityData[0].Id,
                    City = cityData[0],
                    Name = "TESTBUILDING3",
                    SeaDistance = 100,
                    Shore = ShoreType.Sandy,
                    Features = Feature.PrivateParking
                }
			};

            _cityDTOs = cityData.Select(city => new CityDTO
			{
				Id = city.Id,
				Name = city.Name
			}).ToList();

			_buildingDTOs = buildingData.Select(building => new BuildingDTO
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
				Features =  FeatureDTO.Convert(building.Features)
			}).ToList();

            // entitásmodell inicializációja, amihez le kell generálnunk az adatokhoz tartozó gyűjteményeket (táblákat) is
            // ehhez néhány alap információt be kell állítanunk, lehetővé téve, hogy az adatok lekérdezését biztosítsa a rendszer
            IQueryable<City> queryableCityData = cityData.AsQueryable();
            _cityMock = new Mock<DbSet<City>>();
            _cityMock.As<IQueryable<City>>().Setup(mock => mock.ElementType).Returns(queryableCityData.ElementType);
            _cityMock.As<IQueryable<City>>().Setup(mock => mock.Expression).Returns(queryableCityData.Expression);
            _cityMock.As<IQueryable<City>>().Setup(mock => mock.Provider).Returns(queryableCityData.Provider);
            _cityMock.As<IQueryable<City>>().Setup(mock => mock.GetEnumerator()).Returns(cityData.GetEnumerator()); // a korábban megadott listát fogjuk visszaadni

            IQueryable<Building> queryableBuildingData = buildingData.AsQueryable();
            _buildingMock = new Mock<DbSet<Building>>();
            _buildingMock.As<IQueryable<Building>>().Setup(mock => mock.ElementType).Returns(queryableBuildingData.ElementType);
            _buildingMock.As<IQueryable<Building>>().Setup(mock => mock.Expression).Returns(queryableBuildingData.Expression);
            _buildingMock.As<IQueryable<Building>>().Setup(mock => mock.Provider).Returns(queryableBuildingData.Provider);
            _buildingMock.As<IQueryable<Building>>().Setup(mock => mock.GetEnumerator()).Returns(buildingData.GetEnumerator());

            _buildingMock.Setup(mock => mock.Add(It.IsAny<Building>())).Callback<Building>(building =>
            {
                buildingData.Add(building);
            }); // beállítjuk, hogy mi történjen épület hozzáadásakor

            _buildingMock.Setup(mock => mock.Remove(It.IsAny<Building>())).Callback<Building>(building =>
            {
                buildingData.Remove(building);
            }); // beállítjuk, hogy mi történjen épület törlésekor

            // a szimulált entitásmodell csak ezt a két táblát fogja tartalmazni
            _entityMock = new Mock<TravelAgencyContext>();
            _entityMock.Setup<DbSet<Building>>(entity => entity.Buildings).Returns(_buildingMock.Object);
            _entityMock.Setup<DbSet<City>>(entity => entity.Cities).Returns(_cityMock.Object);
        }

        [Fact]
		public void GetCityTest()
		{
			var controller = new CitiesController(_entityMock.Object);
			var result = controller.GetCities();

			// Assert
			var objectResult = Assert.IsType<OkObjectResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<CityDTO>>(objectResult.Value);
			Assert.Equal(_cityDTOs, model);
		}

		[Fact]
		public void GetBuildingTest()
		{
			var controller = new BuildingsController(_entityMock.Object);
			var result = controller.GetBuildings();

			// Assert
			var objectResult = Assert.IsType<OkObjectResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<BuildingDTO>>(objectResult.Value);
			Assert.Equal(_buildingDTOs, model);
		}

        [Fact]
        public void CreateBuildingTest()
        {
            var newBuilding = new BuildingDTO
            {
                City = _cityDTOs[0],
                Name = "TESTBUILDING4",
                SeaDistance = 1000,
                Shore = ShoreType.Rocky,
                Features = FeatureDTO.Convert(Feature.Garden | Feature.SwimmingPool)
            };

            var controller = new BuildingsController(_entityMock.Object);
            var result = controller.PostBuilding(newBuilding);

            // Assert
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<BuildingDTO>(objectResult.Value);
            Assert.Equal(_buildingDTOs.Count + 1, _entityMock.Object.Buildings.Count());
            Assert.Equal(newBuilding, model);
        }

        [Fact]
        public void DeleteBuildingTest()
        {
            var controller = new BuildingsController(_entityMock.Object);
            int deletedId = _entityMock.Object.Buildings.First().Id;
            var result = controller.DeleteBuilding(deletedId);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Equal(_buildingDTOs.Count - 1, _entityMock.Object.Buildings.Count());
            Assert.DoesNotContain(deletedId, _entityMock.Object.Buildings.Select(b => b.Id));
        }
    }
}
