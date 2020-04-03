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

        public AccountService(TravelAgencyContext context)
        {
	        _context = context;
        }

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

            return true;
        }

        /// <summary>
        /// Felhasználó kijelentkeztetése.
        /// </summary>
        public Boolean Logout()
        {
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
                UserPassword = passwordBytes
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