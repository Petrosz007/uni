using ELTE.TravelAgency.Data;
using ELTE.TravelAgency.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ELTE.TravelAgency.Service.Controllers
{
	/// <summary>
	/// Épületek képeinek kezelését biztosító vezérlő.
	/// </summary>
	[Route("api/images")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BuildingImagesController : Controller
    {
		private readonly TravelAgencyContext _context;

	    /// <summary>
	    /// Vezérlő példányosítása.
	    /// </summary>
	    /// <param name="context">Entitásmodell.</param>
	    public BuildingImagesController(TravelAgencyContext context)
	    {
		    if (context == null)
			    throw new ArgumentNullException(nameof(context));

		    _context = context;
	    }

        /// <summary>
        /// Egy épület képeinek lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosító.</param>
        [HttpGet("{buildingId}")]
        public IActionResult GetImages(Int32 buildingId)
        {
            // a megfelelő képeket betöltjük és átalakítjuk (csak a kis képeket)
            return Ok(_context.BuildingImages.Where(image => image.BuildingId == buildingId).Select(image => new ImageDTO { Id = image.Id, ImageSmall = image.ImageSmall }));
        }

        /// <summary>
        /// Egy kép lekérdezése.
        /// </summary>
        [HttpGet("building/{id}")]
        public IActionResult GetImage(Int32 id)
        {
            BuildingImage image = _context.BuildingImages.FirstOrDefault(img => img.BuildingId == id);

            if (image == null)
                return NotFound();

            // a képe átalakítjuk (kis és nagy képet egyaránt)
            return Ok(new ImageDTO 
            { 
                Id = image.Id,
                BuildingId = image.BuildingId,
                ImageSmall = image.ImageSmall, 
                ImageLarge = image.ImageLarge
            });
        }

        /// <summary>
        /// Kép feltöltése.
        /// </summary>
        /// <param name="image">Kép.</param>
        [HttpPost] // itt nem kell paramétereznünk, csak jelezzük, hogy az egyedi útvonalat vesszük igénybe
        [Authorize(Roles = "administrator")] // csak bejelentkezett adminisztrátoroknak
        public IActionResult PostImage([FromBody] ImageDTO image)
        {
            if (image == null || !_context.Buildings.Any(building => image.BuildingId == building.Id))
                return NotFound();

            BuildingImage buildingImage = new BuildingImage
            {
                BuildingId = image.BuildingId,
                ImageSmall = image.ImageSmall,
                ImageLarge = image.ImageLarge
            };

	        _context.BuildingImages.Add(buildingImage);

            try
            {
	            _context.SaveChanges();
                return CreatedAtAction(nameof(GetImage), new { id = buildingImage.Id }, buildingImage.Id); // csak az azonosítót küldjük vissza
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

        /// <summary>
        /// Kép törlése.
        /// </summary>
        /// <param name="id">A kép azonosítója.</param>
        [Route("{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult DeleteImage(Int32 id)
        {
            BuildingImage image = _context.BuildingImages.FirstOrDefault(im => im.Id == id);

            if (image == null)
                return NotFound();

            try
            {
	            _context.BuildingImages.Remove(image);
	            _context.SaveChanges();
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
