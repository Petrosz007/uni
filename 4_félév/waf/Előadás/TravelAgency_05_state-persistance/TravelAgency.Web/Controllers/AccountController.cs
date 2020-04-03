using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ELTE.TravelAgency.Web.Controllers
{
	public class AccountController : BaseController
    {
        /// <summary>
	    /// Vezérlő példányosítása.
	    /// </summary>
		public AccountController(IAccountService accountService, ITravelService travelService)
            : base(accountService, travelService)
        { }

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

            // ha sikeres volt a bejelentkeztetés                  
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
            _accountService.Logout();

            return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }
    }
}