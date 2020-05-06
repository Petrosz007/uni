using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELTE.TravelAgency.Model
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
        /// Ütközik-e egy másik foglalással.
        /// </summary>
        /// <param name="startDate">A foglalás kezdete.</param>
        /// <param name="endDate">A foglalás vége.</param>
        /// <returns>Igaz, ha ütközik, egyébként hamis.</returns>
        public Boolean IsConflicting(DateTime startDate, DateTime endDate)
        {
            return StartDate >= startDate && StartDate < endDate ||
                   startDate >= StartDate && startDate < EndDate;
        }

        /// <summary>
        /// Ütközik-e egy dátummal.
        /// </summary>
        /// <param name="date">A dátum.</param>
        /// <returns>Igaz, ha ütközik, egyébként hamis.</returns>
        public Boolean IsConflicting(DateTime date)
        {
            return IsConflicting(date, date + TimeSpan.FromDays(1));
        }
	}
}
