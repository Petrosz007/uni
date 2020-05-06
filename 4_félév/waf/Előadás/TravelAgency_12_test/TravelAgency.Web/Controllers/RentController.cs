using ELTE.TravelAgency.Model;
using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Web.Controllers
{
	/// <summary>
	/// Foglalások vezérlője.
	/// </summary>
	public class RentController : BaseController
    {
		/// <summary>
		/// Felhasználókezelési szolgáltatás.
		/// </summary>
		private readonly UserManager<Guest> _userManager;

		/// <summary>
		/// Vezérlő példányosítása.
		/// </summary>
		public RentController(ITravelService travelService, ApplicationState applicationState,
			UserManager<Guest> userManager)
			: base(travelService, applicationState)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// Foglalás (oldal lekérése).
		/// </summary>
		/// <param name="apartmentId"></param>
		/// <returns></returns>
		[HttpGet]
        public async Task<IActionResult> Index(Int32? apartmentId)
        {
            // létrehozunk egy foglalást csak az alapadatokkal (apartman, dátumok)
            RentViewModel rent = _travelService.NewRent(apartmentId);

            if (rent == null) // ha nem sikerül (nem volt azonosító)
                return RedirectToAction("Index", "Home"); // visszairányítjuk a főoldalra

	        // ha a felhasználó be van jelentkezve
	        if (User.Identity.IsAuthenticated)
	        {
		        Guest guest = await _userManager.FindByNameAsync(User.Identity.Name);

		        // akkor az adatait közvetlenül is betölthetjük
		        if (guest != null)
		        {
			        rent.GuestAddress = guest.Address;
			        rent.GuestEmail = guest.Email;
			        rent.GuestName = guest.Name;
			        rent.GuestPhoneNumber = guest.PhoneNumber;
		        }
		        // így nem kell újra megadnia
	        }

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
        public async Task<IActionResult> Index(Int32? apartmentId, RentViewModel rent)
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

	        Guest guest;
	        // bejelentkezett felhasználó esetén nem kell felvennünk az új felhasználót
	        if (User.Identity.IsAuthenticated)
	        {
		        guest = await _userManager.FindByNameAsync(User.Identity.Name);
	        }
	        else
	        {
		        guest = new Guest
		        {
			        UserName = "user" + Guid.NewGuid(),
			        Email = rent.GuestEmail,
			        Name = rent.GuestName,
			        Address = rent.GuestAddress,
			        PhoneNumber = rent.GuestPhoneNumber
		        };
		        var result = await _userManager.CreateAsync(guest); // a felhasználónak nem lesz (kezdetben) jelszava
				if (!result.Succeeded)
		        {
			        ModelState.AddModelError("", "A foglalás rögzítése sikertelen, kérem próbálja újra!");
			        return View("Index", rent);
		        }
	        }

			if (!await _travelService.SaveRentAsync(apartmentId, guest.UserName, rent))
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