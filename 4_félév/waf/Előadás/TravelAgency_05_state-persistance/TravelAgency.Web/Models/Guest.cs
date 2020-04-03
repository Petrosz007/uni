using System.Collections.Generic;

namespace ELTE.TravelAgency.Web.Models
{
    public class Guest
    {
        public Guest()
        {
            Rents = new HashSet<Rent>();
        }

	    public int Id { get; set; }
		public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] UserPassword { get; set; }
	    public string UserChallenge { get; set; }

		public ICollection<Rent> Rents { get; set; }
    }
}
