using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ELTE.TravelAgency.Web.Controllers
{
	public class AccountController : Controller
    {
	    // a logikát modell osztályok mögé rejtjük
		private readonly IAccountService _accountService;
        private readonly ITravelService _travelService;

	    /// <summary>
	    /// Vezérlő példányosítása.
	    /// </summary>
		public AccountController(IAccountService accountService, ITravelService travelService)
        {
	        _accountService = accountService;
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

		public IActionResult Index()
        {
			return RedirectToAction("Login");
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
			return View("Login");
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="user">A bejelentkezés adatai.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel user)
        {
			if (!ModelState.IsValid)
                return View("Login", user);

            // bejelentkeztetjük a felhasználót
            if (!_accountService.Login(user))
            {
                // nem szeretnénk, ha a felhasználó tudná, hogy a felhasználónévvel, vagy a jelszóval van-e baj, így csak általános hibát jelzünk
                ModelState.AddModelError("", "Hibás felhasználónév, vagy jelszó.");
                return View("Login", user);
            }

            // ha sikeres volt az ellenőrzés
	        HttpContext.Session.SetString("user", user.UserName); // felvesszük a felhasználó nevét a munkamenetbe                    
            return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }

        /// <summary>
        /// Regisztráció.
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
			return View("Register");
        }

        /// <summary>
        /// Regisztráció.
        /// </summary>
        /// <param name="guest">Regisztrációs adatok.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationViewModel guest)
        {
			// végrehajtjuk az ellenőrzéseket
			if (!ModelState.IsValid)
                return View("Register", guest);

            if (!_accountService.Register(guest))
            {
                ModelState.AddModelError("UserName", "A megadott felhasználónév már létezik.");
                return View("Register", guest);
            }

            ViewBag.Information = "A regisztráció sikeres volt. Kérjük, jelentkezzen be.";

            if (HttpContext.Session.GetString("user") != null) // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
                HttpContext.Session.Remove("user");

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        public IActionResult Logout()
        {
			if (HttpContext.Session.GetString("user") != null) // ha be volt jelentkezve egy felhasználó, akkor kijelentkeztetjük
				HttpContext.Session.Remove("user");

            return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }
    }
}