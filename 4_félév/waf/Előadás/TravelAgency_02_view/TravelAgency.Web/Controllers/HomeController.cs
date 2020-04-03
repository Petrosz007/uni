using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace ELTE.TravelAgency.Web.Controllers
{
	/// <summary>
	/// Vezérlő típusa
	/// </summary>
	public class HomeController : Controller
	{
		private readonly TravelAgencyContext _context;
		private readonly IOptions<GoogleConfig> _googleConfig;

		/// <summary>
		/// Vezérlő példányosítása.
		/// </summary>
		public HomeController(TravelAgencyContext context, IOptions<GoogleConfig> googleConfig)
		{
			_context = context;
			_googleConfig = googleConfig;
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
			return View("Index", _context.Buildings.Include(b => b.City).ToList());
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
			return View("Index", _context.Buildings.Include(b => b.City).Where(b => b.CityId == cityId).ToList());
		}

		/// <summary>
		/// Épület részleteinek nézete.
		/// </summary>
		/// <param name="buildingId">Épület azonosítója.</param>
		/// <returns>Az épület részletes nézete.</returns>
		public IActionResult Details(Int32? buildingId)
		{
			if (buildingId == null) // nem adtak meg azonosítót
				return RedirectToAction(nameof(Index));

			Building building = _context.Buildings
				.Include(b => b.City)
				.Include(b => b.Apartments)
				.FirstOrDefault(b => b.Id == buildingId);

			if (building == null)
				return NotFound(); // átirányítjuk a nem talált oldalra

			// az oldal címe
			ViewBag.Title = "Épület részletei: " + building.Name + " (" + building.City.Name + ")";
			// az épülethez tartozó képek azonosítói
			ViewBag.Images = _context.BuildingImages.Where(image => image.BuildingId == buildingId).Select(image => image.Id).ToList();
			// Google Maps API Key
			ViewBag.GoogleMapsApiKey = _googleConfig.Value.MapsApiKey;

			return View("Details", building);
		}

		/// <summary>
		/// Épület főképének lekérdezése.
		/// </summary>
		/// <param name="buildingId">Épület azonosítója.</param>
		/// <returns>Az épület képe, vagy az alapértelmezett kép.</returns>
		public FileResult ImageForBuilding(Int32? buildingId)
		{
			if (buildingId == null) // nem adtak meg azonosítót
				return File("~/images/NoImage.png", "image/png");

			// lekérjük az épület első tárolt képjét (kicsiben)
			Byte[] imageContent = _context.BuildingImages
				.Where(image => image.BuildingId == buildingId)
				.Select(image => image.ImageSmall)
				.FirstOrDefault();

			if (imageContent == null) // amennyiben nem sikerült betölteni, egy alapértelmezett képet adunk vissza
				return File("~/images/NoImage.png", "image/png");

			return File(imageContent, "image/png");
		}

		/// <summary>
		/// Épület egyik képének lekérdezése.
		/// </summary>
		/// <param name="imageId">Kép azonosítója.</param>
		/// <param name="large">Nagy méretű kép lekérése.</param>
		/// <returns>Az épület egy képe, vagy az alapértelmezett kép.</returns>
		public FileResult Image(Int32? imageId, Boolean large = false)
		{
			if (imageId == null) // nem adtak meg azonosítót
				return File("~/images/NoImage.png", "image/png");

			// lekérjük a megadott azonosítóval rendelkező képet
			Byte[] imageContent = _context.BuildingImages
				.Where(image => image.Id == imageId)
				.Select(image => large ? image.ImageLarge : image.ImageSmall)
				.FirstOrDefault();

			if (imageContent == null) // amennyiben nem sikerült betölteni, egy alapértelmezett képet adunk vissza
				return File("~/images/NoImage.png", "image/png");

			return File(imageContent, "image/png");
		}
	}
}