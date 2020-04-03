using System;
using System.Linq;

namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Foglalás dátumát ellenőrző típus.
    /// </summary>
    public class RentDateValidator
    {
	    private readonly TravelAgencyContext _context;

		public RentDateValidator(TravelAgencyContext context)
		{
			_context = context;
		}

        /// <summary>
        /// Foglalás dátumainak ellenőrzése.
        /// </summary>
        /// <param name="start">Foglalás kezdete.</param>
        /// <param name="end">Foglalás vége.</param>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        public RentDateError Validate(DateTime start, DateTime end, Int32 apartmentId)
        {
            if (start < DateTime.Now + TimeSpan.FromDays(7)) // korai kezdés
                return RentDateError.StartInvalid;

            if (end < start)
                return RentDateError.EndInvalid;

            if (end == start) // üres foglalás 
                return RentDateError.LengthInvalid;

            if (Convert.ToInt32((end - end).TotalDays) % 7 != 0) // nem egész hetet foglalt
                return RentDateError.LengthInvalid;
			
            Apartment selectedApartment = _context.Apartments.FirstOrDefault(apartment => apartment.Id == apartmentId);

            if (selectedApartment == null)
                return RentDateError.None;

            if (start.DayOfWeek != selectedApartment.Turnday) // nem fordulónapos kezdés
                return RentDateError.StartInvalid;

            if (_context.Rents.Where(r => r.ApartmentId == selectedApartment.Id && r.EndDate >= start)
                                .ToList()
                                .Any(r => r.IsConflicting(start, end))) // az időszakra már van foglalás
                return RentDateError.Conflicting;

            return RentDateError.None; // ha ideág eljutunk, nem találtunk hibát.
        }
    }
}