using ELTE.TravelAgency.Data;
using ELTE.TravelAgency.Service.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELTE.TravelAgency.Service.Controllers
{
    /// <summary>
    /// Városok lekérdezését biztosító vezérlő.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CitiesController : ControllerBase
    {
		private readonly TravelAgencyContext _context;

		/// <summary>
		/// Vezérlő példányosítása.
		/// </summary>
		public CitiesController(TravelAgencyContext context)
	    {
		    if (context == null)
			    throw new ArgumentNullException(nameof(context));

		    _context = context;
	    }

        /// <summary>
        /// Városok lekérdezése.
        /// </summary>
        [HttpGet]
        public IActionResult GetCities()
        {
            try
            {
                return Ok(_context.Cities.ToList().Select(city => new CityDTO
                {
                    Id = city.Id,
                    Name = city.Name
                }));
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }
    }
}
