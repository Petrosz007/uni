using ELTE.TravelAgency.Model;
using ELTE.TravelAgency.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Web.Controllers
{
	public class AccountController : BaseController
    {
		/// <summary>
		/// Felhasználókezelési szolgáltatás.
		/// </summary>
		private readonly UserManager<Guest> _userManager;
		/// <summary>
		/// Authentikációs szolgáltatás.
		/// </summary>
		private readonly SignInManager<Guest> _signInManager;
		
		public AccountController(ITravelService travelService, ApplicationState applicationState,
			UserManager<Guest> userManager, SignInManager<Guest> signInManager)
		    : base(travelService, applicationState)
		{
			_userManager = userManager;
			_signInManager = signInManager;
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
        public async Task<IActionResult> Login(LoginViewModel user)
        {
			if (!ModelState.IsValid)
                return View("Login", user);

            // bejelentkeztetjük a felhasználót
	        var result = await _signInManager.PasswordSignInAsync(user.UserName, user.UserPassword, user.RememberLogin, false);
			if(!result.Succeeded)
            {
                // nem szeretnénk, ha a felhasználó tudná, hogy a felhasználónévvel, vagy a jelszóval van-e baj, így csak általános hibát jelzünk
                ModelState.AddModelError("", "Hibás felhasználónév, vagy jelszó.");
                return View("Login", user);
            }

			// ha sikeres volt az ellenőrzés


			// ha sikeres volt az ellenőrzés, akkor a SignInManager már beállította a munkamenetet      
	        _applicationState.UserCount++; // módosítjuk a felhasználók számát
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
		/// <param name="user">Regisztrációs adatok.</param>
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel user)
        {
			// végrehajtjuk az ellenőrzéseket
			if (!ModelState.IsValid)
                return View("Register", user);

	        Guest guest = new Guest
	        {
				UserName = user.UserName,
				Email = user.GuestEmail,
				Name = user.GuestName,
				Address = user.GuestAddress,
				PhoneNumber = user.GuestPhoneNumber
	        };
	        var result = await _userManager.CreateAsync(guest, user.UserPassword);
	        if (!result.Succeeded)
	        {
		        // Felvesszük a felhasználó létrehozásával kapcsolatos hibákat.
				foreach (var error in result.Errors)
			        ModelState.AddModelError("", error.Description);
                return View("Register", user);
            }

	        await _signInManager.SignInAsync(guest, false); // be is jelentkeztetjük egyből a felhasználót
	        _applicationState.UserCount++; // módosítjuk a felhasználók számát
			return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
		}

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        public async Task<IActionResult> Logout()
        {
	        await _signInManager.SignOutAsync();

	        _applicationState.UserCount--; // módosítjuk a felhasználók számát
			return RedirectToAction("Index", "Home"); // átirányítjuk a főoldalra
        }
    }
}