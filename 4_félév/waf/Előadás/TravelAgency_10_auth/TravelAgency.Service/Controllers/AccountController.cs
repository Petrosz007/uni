using ELTE.TravelAgency.Service.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELTE.TravelAgency.Service.Controllers
{
	/// <summary>
	/// Felhasználókezelést biztosító vezérlő.
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : Controller
    {
	    /// <summary>
	    /// Authentikációs szolgáltatás.
	    /// </summary>
	    private readonly SignInManager<Guest> _signInManager;

		/// <summary>
		/// Vezérlő példányosítása.
		/// </summary>
		public AccountController(SignInManager<Guest> signInManager)
        {
	        _signInManager = signInManager;
		}

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="userName">Felhasználónév.</param>
        /// <param name="userPassword">Jelszó.</param>
        [HttpGet("login/{userName}/{userPassword}")]
        public async Task<IActionResult> Login(String userName, String userPassword)
        {
            try
            {
	            // bejelentkeztetjük a felhasználót
	            var result = await _signInManager.PasswordSignInAsync(userName, userPassword, false, false);
	            if (!result.Succeeded) // ha nem sikerült, akkor nincs bejelentkeztetés
		            return Forbid();

				// ha sikeres volt az ellenőrzés
                return Ok();
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        [HttpGet("logout")]
        [Authorize] // csak bejelentkezett felhasználóknak
        public async Task<IActionResult> Logout()
        {
            try
            {
				// kijelentkeztetjük az aktuális felhasználót
				await _signInManager.SignOutAsync();
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
