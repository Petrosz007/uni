using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ELTE.TravelAgency.Model
{
	/// <summary>
	/// Vend�g.
	/// </summary>
	public class Guest : IdentityUser<int>
    {
        public Guest()
        {
            Rents = new HashSet<Rent>();
        }

		/* A kor�bban defini�lt tulajdons�gok k�z�l az IdentityUser<T> tartalmazza:
		 * T Id
		 * string UserName
		 * string PasswordHash (UserPassword helyett)
		 * string Email
		 * string PhoneNumber
		 * string SecurityStamp (UserChallenge helyett)
		 */

	    /// <summary>
	    /// Teljes n�v.
	    /// </summary>
		public string Name { get; set; }

	    /// <summary>
	    /// C�m.
	    /// </summary>
		public string Address { get; set; }


		/// <summary>
		/// Foglal�sok.
		/// </summary>
		public ICollection<Rent> Rents { get; set; }
    }
}
