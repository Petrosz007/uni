using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ELTE.TravelAgency.Web.Models
{
	/// <summary>
	/// Felhasználókezelési szolgáltatás típusa.
	/// </summary>
	public class AccountService : IAccountService
    {
        private readonly TravelAgencyContext _context;
	    private readonly HttpContext _httpContext;
	    private readonly ApplicationState _applicationState;

        public AccountService(TravelAgencyContext context, 
	        IHttpContextAccessor httpContextAccessor, ApplicationState applicationState)
        {
	        _context = context;
	        _httpContext = httpContextAccessor.HttpContext;
	        _applicationState = applicationState;

	        // ha a felhasználónak van sütije, de még nincs bejelentkezve, bejelentkeztetjük
	        if (_httpContext.Request.Cookies.ContainsKey("user_challenge") &&
	            !_httpContext.Session.Keys.Contains("user"))
	        {
		        Guest guest = _context.Guests.FirstOrDefault(
			        g => g.UserChallenge == _httpContext.Request.Cookies["user_challenge"]);
		        // kikeressük a felhasználót
		        if (guest != null)
		        {
			        _httpContext.Session.SetString("user", guest.UserName);
			        // felvesszük a felhasználó nevét a munkamenetbe

			        UserCount++; // növeljük a felhasználószámot
		        }
	        }
		}

		/// <summary>
		/// Felhasználószám lekérdezése.
		/// 
		/// A felhasználószámot globális állapotként tároljuk.
		/// </summary>
		public Int32 UserCount
	    {
		    get => (Int32)_applicationState.UserCount;
		    set => _applicationState.UserCount = value;
	    }

	    /// <summary>
	    /// Aktuálisan bejelentkezett felhasználó nevének lekérdezése.
	    /// </summary>
	    public String CurrentUserName => _httpContext.Session.GetString("user");

	    /// <summary>
		/// Vendégadatok lekérdezése.
		/// </summary>
		/// <param name="userName">A felhasználónév.</param>
		public Guest GetGuest(String userName)
        {
            if (userName == null)
                return null;

            return _context.Guests.FirstOrDefault(c => c.UserName == userName); // megkeressük a vendéget
        }

        /// <summary>
        /// Felhasználó bejelentkeztetése.
        /// </summary>
        /// <param name="user">A felhasználó nézetmodellje.</param>
        public Boolean Login(LoginViewModel user)
        {
            if (user == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(user, new ValidationContext(user, null, null), null))
                return false;

            Guest guest = _context.Guests.FirstOrDefault(c => c.UserName == user.UserName); // megkeressük a felhasználót

            if (guest == null)
                return false;

            // ellenőrizzük a jelszót (ehhez a kapott jelszót hash-eljük)
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
            }

            if (!passwordBytes.SequenceEqual(guest.UserPassword))
                return false;

	        // ha sikeres volt az ellenőrzés
	        _httpContext.Session.SetString("user", user.UserName); // felvesszük a felhasználó nevét a munkamenetbe

	        if (user.RememberLogin) // ha meg kell jegyeznünk a felhasználót
	        {
		        // akkor elküldjük azt sütiként
				_httpContext.Response.Cookies.Append("user_challenge",  guest.UserChallenge, // nem a felhasználónevet, hanem egy generált kódot tárolunk
					new CookieOptions
			        {
						Expires = DateTime.Today.AddDays(365), // egy évig lesz érvényes a süti
						HttpOnly = true, // igyekszünk biztonságosság tenni a sütit
						//Secure = true,
					});
	        }

	        // ha sikeres volt az ellenőrzés
	        // módosítjuk a felhasználók számát
	        UserCount++;

			return true;
        }

        /// <summary>
        /// Felhasználó kijelentkeztetése.
        /// </summary>
        public Boolean Logout()
        {
	        if (!_httpContext.Session.Keys.Contains("user"))
		        return false;

	        // töröljük a munkafolyamatból
			_httpContext.Session.Remove("user");

	        // kitöröljük a sütit (amennyiben volt)
			_httpContext.Response.Cookies.Delete("user_challenge");
	        // Ezzel valójában lejártnak minősítjük (Expires = DateTime.MinValue)

			// módosítjuk a felhasználók számát
			UserCount--;

			return true;
        }

        /// <summary>
        /// Vendég regisztrációja.
        /// </summary>
        /// <param name="guest">A vendég nézetmodellje.</param>
        public Boolean Register(RegistrationViewModel guest)
        {
            if (guest == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(guest, new ValidationContext(guest, null, null), null))
                return false;

            if (_context.Guests.Count(c => c.UserName == guest.UserName) != 0)
                return false;

            // kódoljuk a jelszót
            Byte[] passwordBytes = null;
            using (SHA512CryptoServiceProvider provider = new SHA512CryptoServiceProvider())
            {
                passwordBytes = provider.ComputeHash(Encoding.UTF8.GetBytes(guest.UserPassword));
            }

            // elmentjük a felhasználó adatait
            _context.Guests.Add(new Guest
            {
                Name = guest.GuestName,
                Address = guest.GuestAddress,
                Email = guest.GuestEmail,
                PhoneNumber = guest.GuestPhoneNumber,
                UserName = guest.UserName,
                UserPassword = passwordBytes,
	            UserChallenge = Guid.NewGuid().ToString()
			});

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Vendég létrehozása (regisztráció nélkül).
        /// </summary>
        /// <param name="guest">A vendég nézetmodellje.</param>
        /// <param name="userName">A felhasználónév.</param>
        public Boolean Create(GuestViewModel guest, out String userName)
        {
            userName = "user" + Guid.NewGuid(); // a felhasználónevet generáljuk

            if (guest == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(guest, new ValidationContext(guest, null, null), null))
                return false;

            // elmentjük a felhasználó adatait
            _context.Guests.Add(new Guest
            {
                Name = guest.GuestName,
                Address = guest.GuestAddress,
                Email = guest.GuestEmail,
                PhoneNumber = guest.GuestPhoneNumber,
                UserName = userName
            });
            
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}