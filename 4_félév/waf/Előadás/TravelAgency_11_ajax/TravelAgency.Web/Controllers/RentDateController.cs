using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELTE.TravelAgency.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentDateController : ControllerBase
    {

        private ITravelService _service;

        public RentDateController(ITravelService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adott adott hónap szabad napjainak lekérdezése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosító.</param>
        /// <param name="year">Az év.</param>
        /// <param name="month">A hónap.</param>
        /// <returns>Az adott hónap szabad napjai egy gyűjteményben.</returns>
        [Route("{apartmentId}/{year}/{month}")] // útvonal feloldás megadása
        public IActionResult Get(Int32? apartmentId, Int32? year, Int32? month)
        {
            if (apartmentId == null || year == null || month == null)
                return BadRequest(); // 400-as válaszkód

            return Ok(_service.GetRentDates(apartmentId.Value, year.Value, month.Value)); // 200-as válaszkód
        }
    }
}