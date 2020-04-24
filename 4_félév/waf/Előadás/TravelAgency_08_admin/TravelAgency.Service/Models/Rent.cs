using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELTE.TravelAgency.Service.Models
{
    public class Rent
    {
	    public int Id { get; set; }
		[ForeignKey("Guest")]
		public int UserId { get; set; }
        public int ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    
        public Apartment Apartment { get; set; }
        public Guest Guest { get; set; }

	    /// <summary>
	    /// �tk�zik-e egy m�sik foglal�ssal.
	    /// </summary>
	    /// <param name="startDate">A foglal�s kezdete.</param>
	    /// <param name="endDate">A foglal�s v�ge.</param>
	    /// <returns>Igaz, ha �tk�zik, egy�bk�nt hamis.</returns>
	    public Boolean IsConflicting(DateTime startDate, DateTime endDate)
	    {
		    return StartDate >= startDate && StartDate < endDate ||
		           EndDate >= startDate && EndDate < endDate ||
		           StartDate < startDate && EndDate > endDate ||
		           StartDate > startDate && EndDate < endDate;
	    }
	}
}
