using ELTE.TravelAgency.Data;
using ELTE.TravelAgency.Service.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELTE.TravelAgency.Service.Controllers
{
    /// <summary>
    /// Épületek lekérdezését és módosítását biztosító vezérlő.
    /// </summary>
    [ApiController] // API vezérlő osztály: automatikus modell validáció és további auto-konfiguráció: https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1#apicontroller-attribute
    [Route("api/[controller]")] // Routing szabály
    [ApiConventionType(typeof(DefaultApiConventions))] // OpenAPI konvenciók alkalmazása az akciók által visszaadható HTTP státusz kódokra
    public class BuildingsController : ControllerBase
    {
        private readonly TravelAgencyContext _context;
        
        /// <summary>
        /// Vezérlő példányosítása.
        /// </summary>
        /// <param name="context">Entitásmodell.</param>
        public BuildingsController(TravelAgencyContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

	        _context = context;
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        [HttpGet]
        public IActionResult GetBuildings()
        {
            try
            {
                return Ok(_context.Buildings
                    .Include(b => b.City)
                    .ToList()
                    .Select(building => new BuildingDTO
                {
                    Id = building.Id,
                    Name = building.Name,
                    City = new CityDTO { Id = building.City.Id, Name = building.City.Name },
                    SeaDistance = building.SeaDistance,
                    Shore = building.Shore,
                    Features = FeatureDTO.Convert(building.Features),
                    LocationX = building.LocationX,
                    LocationY = building.LocationY,
                    Comment = building.Comment
                }));
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Épület lekérdezése.
        /// </summary>
        /// <param name="id">Épület azonosító.</param>
        [HttpGet("{id}")]
        public IActionResult GetBuilding(Int32 id)
        {
            try
            {
                var building = _context.Buildings
                    .Include(b => b.City)
                    .Single(b => b.Id == id);
                return Ok(new BuildingDTO
                {
                    Id = building.Id,
                    Name = building.Name,
                    City = new CityDTO { Id = building.City.Id, Name = building.City.Name },
                    SeaDistance = building.SeaDistance,
                    Shore = building.Shore,
                    Features = FeatureDTO.Convert(building.Features),
                    LocationX = building.LocationX,
                    LocationY = building.LocationY,
                    Comment = building.Comment
                });
            }
            catch (InvalidOperationException)
            {
                // Single() nem adott eredményt
                return NotFound();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        /// <param name="id">Város azonosító.</param>
        [HttpGet("city/{id}")]
		public IActionResult GetBuildingForCity(Int32 id)
        {
            try
            {
                return Ok(_context.Buildings
                    .Include(b => b.City)
                    .Where(b => b.CityId == id)
                    .ToList()
                    .Select(building => new BuildingDTO
                {
                    Id = building.Id,
                    Name = building.Name,
                    City = new CityDTO { Id = building.City.Id, Name = building.City.Name },
                    SeaDistance = building.SeaDistance,
                    Shore = building.Shore,
                    Features = FeatureDTO.Convert(building.Features),
                    LocationX = building.LocationX,
                    LocationY = building.LocationY,
                    Comment = building.Comment
                }));
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

        /// <summary>
        /// Új épület létrehozása.
        /// </summary>
        /// <param name="buildingDTO">Épület.</param>
        [HttpPost]
        [Authorize(Roles = "administrator")]
        public IActionResult PostBuilding([FromBody] BuildingDTO buildingDTO)
        {
            try
            {
                var addedBuilding = _context.Buildings.Add(new Building
                {
                    Name = buildingDTO.Name,
                    CityId = buildingDTO.City.Id,
                    SeaDistance = buildingDTO.SeaDistance,
                    Shore = buildingDTO.Shore,
                    Features = FeatureDTO.Convert(buildingDTO.Features),
                    LocationX = buildingDTO.LocationX,
                    LocationY = buildingDTO.LocationY,
                    Comment = buildingDTO.Comment
                });

                _context.SaveChanges(); // elmentjük az új épületet

	            buildingDTO.Id = addedBuilding.Entity.Id;

                // visszaküldjük a létrehozott épületet
                return CreatedAtAction(nameof(GetBuilding), new {id = addedBuilding.Entity.Id}, buildingDTO);
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

        /// <summary>
        /// Épület módosítása.
        /// </summary>
        /// <param name="buildingDTO">Épület.</param>
        [HttpPut]
        [Authorize(Roles = "administrator")]
        public IActionResult PutBuilding([FromBody] BuildingDTO buildingDTO)
        {
            try
            {
                Building building = _context.Buildings.FirstOrDefault(b => b.Id == buildingDTO.Id);

                if (building == null) // ha nincs ilyen azonosító, akkor hibajelzést küldünk
                    return NotFound();

                building.Name = buildingDTO.Name;
                building.CityId = buildingDTO.City.Id;
                building.SeaDistance = buildingDTO.SeaDistance;
                building.Shore = buildingDTO.Shore;
                building.Features = FeatureDTO.Convert(buildingDTO.Features);
                building.LocationX = buildingDTO.LocationX;
                building.LocationY = buildingDTO.LocationY;
                building.Comment = buildingDTO.Comment;

                _context.SaveChanges(); // elmentjük a módosított épületet

                return Ok();
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

		/// <summary>
		/// Épület törlése.
		/// </summary>
		/// <param name="id">Épület azonosító.</param>
		[HttpDelete("{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult DeleteBuilding(Int32 id)
        {
            try
            {
                Building building = _context.Buildings.FirstOrDefault(b => b.Id == id);

                if (building == null) // ha nincs ilyen azonosító, akkor hibajelzést küldünk
                    return NotFound();

	            _context.Buildings.Remove(building);

	            _context.SaveChanges(); // elmentjük a módosított épületet

                return Ok();
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }
    }
}
