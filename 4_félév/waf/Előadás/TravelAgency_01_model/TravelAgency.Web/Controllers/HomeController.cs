using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ELTE.TravelAgency.Web.Controllers
{
	/// <summary>
	/// Vezérlő típusa
	/// </summary>
	public class HomeController : Controller
	{
		private TravelAgencyContext _context;

		/// <summary>
		/// Vezérlő példányosítása.
		/// </summary>
		public HomeController(TravelAgencyContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Egy akció meghívása után végrehajtandó metódus.
		/// </summary>
		/// <param name="context">Az akció kontextus argumentuma.</param>
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);

			// a városok listája
			ViewBag.Cities = _context.Cities.ToArray();
		}

		/// <summary>
		/// Épületek listázása.
		/// </summary>
		/// <returns>Az épületek listájának nézete.</returns>
		public IActionResult Index()
		{
			return View("Index", _context.Buildings.Include(b => b.City));
		}


		/// <summary>
		/// Épületek listázása.
		/// </summary>
		/// <param name="cityId">Város azonosítója.</param>
		/// <returns>Az épületek listájának nézete.</returns>
		public IActionResult List(Int32 cityId)
		{
			// ha hibás az azonosító
			if (!_context.Cities.Any(c => c.Id == cityId))
				return NotFound(); // átirányítjuk a nem talált oldalra
			
			// megkeressük a megfelelő város azonosítókat 
			return View("Index", _context.Buildings.Include(b => b.City).Where(b => b.CityId == cityId));
		}

		/// <summary>
		/// Épület részleteinek nézete.
		/// </summary>
		/// <param name="buildingId">Épület azonosítója.</param>
		/// <returns>Az épület részletes nézete.</returns>
		public IActionResult Details(Int32 buildingId)
		{
			Building building = _context.Buildings.Include(b => b.City).Include(b => b.Apartments).FirstOrDefault(b => b.Id == buildingId);

			if (building == null)
				return NotFound(); // átirányítjuk a nem talált oldalra

			// az oldal címe
			ViewBag.Title = "Épület részletei: " + building.Name + " (" + building.City.Name + ")";

			return View("Details", building);
		}
	}
}