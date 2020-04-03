using System;

namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Felhasználókezelési szolgáltatás felülete.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Vendégadatok lekérdezése.
        /// </summary>
        /// <param name="userName">A felhasználónév.</param>
        Guest GetGuest(String userName);

        /// <summary>
        /// Felhasználó bejelentkeztetése.
        /// </summary>
        /// <param name="user">A felhasználó nézetmodellje.</param>
        Boolean Login(LoginViewModel user);

        /// <summary>
        /// Felhasználó kijelentkeztetése.
        /// </summary>
        Boolean Logout();

        /// <summary>
        /// Vendég regisztrációja.
        /// </summary>
        /// <param name="guest">A vendég nézetmodellje.</param>
        Boolean Register(RegistrationViewModel guest);

        /// <summary>
        /// Vendég létrehozása (regisztráció nélkül).
        /// </summary>
        /// <param name="guest">A vendég nézetmodellje.</param>
        /// <param name="userName">A felhasználónév.</param>
        Boolean Create(GuestViewModel guest, out String userName);
    }
}
