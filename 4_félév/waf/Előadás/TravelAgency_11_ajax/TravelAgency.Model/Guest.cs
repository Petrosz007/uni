using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ELTE.TravelAgency.Model
{
	/// <summary>
	/// Vendég.
	/// </summary>
	public class Guest : IdentityUser<int>
    {
        public Guest()
        {
            Rents = new HashSet<Rent>();
        }

		/* A korábban definiált tulajdonságok közül az IdentityUser<T> tartalmazza:
		 * T Id
		 * string UserName
		 * string PasswordHash (UserPassword helyett)
		 * string Email
		 * string PhoneNumber
		 * string SecurityStamp (UserChallenge helyett)
		 */

	    /// <summary>
	    /// Teljes név.
	    /// </summary>
		public string Name { get; set; }

	    /// <summary>
	    /// Cím.
	    /// </summary>
		public string Address { get; set; }


		/// <summary>
		/// Foglalások.
		/// </summary>
		public ICollection<Rent> Rents { get; set; }
    }
}
