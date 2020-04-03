using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ELTE.TravelAgency.Web.Controllers
{
	/// <summary>
	/// Foglalások vezérlője.
	/// </summary>
	public class RentController : Controller
    {
        // a logikát egy modell osztály mögé rejtjük
        private readonly ITravelService _travelService;

        /// <summary>
        /// Vezérlő példányosítása.
        /// </summary>
        public RentController(ITravelService travelService)
        {
	        _travelService = travelService;
        }

	    /// <summary>
	    /// Egy akció meghívása után végrehajtandó metódus.
	    /// </summary>
	    /// <param name="context">Az akció kontextus argumentuma.</param>
	    public override void OnActionExecuted(ActionExecutedContext context)
	    {
		    base.OnActionExecuted(context);

		    // a városok listája
		    ViewBag.Cities = _travelService.Cities.ToArray();
	    }

		/// <summary>
		/// Foglalás (oldal lekérése).
		/// </summary>
		/// <param name="apartmentId"></param>
		/// <returns></returns>
		[HttpGet]
        public IActionResult Index(Int32? apartmentId)
        {
            // létrehozunk egy foglalást csak az alapadatokkal (apartman, dátumok)
            RentViewModel rent = _travelService.NewRent(apartmentId);

            if (rent == null) // ha nem sikerül (nem volt azonosító)
                return RedirectToAction("Index", "Home"); // visszairányítjuk a főoldalra

			return View("Index", rent);
        }

        /// <summary>
        /// Foglalás (adatok beküldése).
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        /// <param name="rent">Foglalás adatai.</param>
        /// <returns>Foglalás eredmény nézete.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // védelem XSRF támadás ellen
        public IActionResult Index(Int32? apartmentId, RentViewModel rent)
        {
            if (apartmentId == null || rent == null)
                return RedirectToAction("Index", "Home");

            rent.Apartment = _travelService.GetApartment(apartmentId);

            if (rent.Apartment == null)
                return RedirectToAction("Index", "Home");

            switch (_travelService.ValidateRent(rent.RentStartDate, rent.RentEndDate, apartmentId.Value))
            {
                case RentDateError.StartInvalid:
                    ModelState.AddModelError("RentStartDate", "A kezdés dátuma nem megfelelő (túl korai, vagy nem fordulónapra esik)!");
                    break;
                case RentDateError.EndInvalid:
                    ModelState.AddModelError("RentEndDate", "A megadott foglalási idő érvénytelen (a foglalás vége korábban van, mint a kezdete)!");
                    break;
                case RentDateError.LengthInvalid:
                    ModelState.AddModelError("RentEndDate", "A megadott foglalási idő érvénytelen (egész heteket lehet csak foglalni)!");
                    break;
                case RentDateError.Conflicting:
                    ModelState.AddModelError("RentStartDate", "A megadott időpontban a szállás már foglalt!");
                    break;
            }

			if (!ModelState.IsValid)
                return View("Index", rent);

            if (!_travelService.SaveRent(apartmentId, rent))
            {
                ModelState.AddModelError("", "A foglalás rögzítése sikertelen, kérem próbálja újra!");
                return View("Index", rent);
            }

            // kiszámoljuk a teljes árat
            rent.TotalPrice = _travelService.GetPrice(apartmentId, rent);

			ViewBag.Message = "A foglalását sikeresen rögzítettük!";
            return View("Result", rent);
        }
    }
}