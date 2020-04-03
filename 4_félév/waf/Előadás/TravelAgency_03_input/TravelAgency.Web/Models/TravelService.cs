using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Az utazással kapcsolatos szolgáltatások típusa.
    /// </summary>
    public class TravelService : ITravelService
    {
        private readonly TravelAgencyContext _context;
	    private readonly RentDateValidator _rentDateValidator;

        public TravelService(TravelAgencyContext context)
        {
	        _context = context;
			_rentDateValidator = new RentDateValidator(_context);
        }

		/// <summary>
		/// Városok lekérdezése.
		/// 
		/// Betöltjük az épületek mellett a városok adatait is.
		/// </summary>
		public IEnumerable<Building> Buildings => _context.Buildings.Include(b => b.City);

		/// <summary>
		/// Épületek lekérdezése.
		/// </summary>
		public IEnumerable<City> Cities => _context.Cities;

		/// <summary>
		/// Épület lekérdezése.
		/// 
		/// Betöltjük az épület mellett a város és az apartmanjai adatait is.
		/// </summary>
		/// <param name="buildingId">Épület azonosítója.</param>
		public Building GetBuilding(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            return _context.Buildings
	            .Include(b => b.City)
	            .Include(b => b.Apartments)
	            .FirstOrDefault(building => building.Id == buildingId);
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        /// <param name="cityId">Város azonosítója.</param>
        public IEnumerable<Building> GetBuildings(Int32? cityId)
        {
            if (cityId == null || !_context.Buildings.Any(building => building.CityId == cityId))
                return null;

            return _context.Buildings.Where(building => building.CityId == cityId);
        }

        /// <summary>
        /// Épület lekérdezése apartmanokkal.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public Building GetBuildingWithApartments(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            return _context.Buildings
	            .Include(b => b.City)
	            .Include(b => b.Apartments)
	            .FirstOrDefault(b => b.Id == buildingId);
        }

        /// <summary>
        /// Épület képek azonosítóinak lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public IEnumerable<Int32> GetBuildingImageIds(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            return _context.BuildingImages
	            .Where(image => image.BuildingId == buildingId)
	            .Select(image => image.Id);
        }

        /// <summary>
        /// Épület főképének lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        public Byte[] GetBuildingMainImage(Int32? buildingId)
        {
            if (buildingId == null)
                return null;

            // lekérjük az épület első tárolt képjét (kicsiben)
            return _context.BuildingImages
	            .Where(image => image.BuildingId == buildingId)
	            .Select(image => image.ImageSmall)
	            .FirstOrDefault();
        }

        /// <summary>
        /// Épület képének lekérdezése.
        /// </summary>
        /// <param name="imageId">Kép azonosítója.</param>
        /// <param name="large">Nagy kép letöltése.</param>
        public Byte[] GetBuildingImage(Int32? imageId, Boolean large)
        {
            if (imageId == null)
                return null;

			// lekérjük a képet a kért méretben (kicsi, nagy)
	        Byte[] imageContent = _context.BuildingImages
				.Where(image => image.Id == imageId)
	            .Select(image => large ? image.ImageLarge : image.ImageSmall)
	            .FirstOrDefault();

			// Amennyiben a kép a megadott azonosítóval nem létezett, null-lal térünk vissza
	        return imageContent;
        }

        /// <summary>
        /// Apartman lekérdezése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        public Apartment GetApartment(Int32? apartmentId)
        {
            if (apartmentId == null)
                return null;

            return _context.Apartments
				.Include(a => a.Building) // betöltjük az apartmanhoz az épületeket
	            .ThenInclude(b => b.City) // az épülethez pedig a várost
				.FirstOrDefault(apartment => apartment.Id == apartmentId);

		}

        /// <summary>
        /// Foglalás létrehozása.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        public RentViewModel NewRent(Int32? apartmentId)
        {
            if (apartmentId == null)
                return null;

            Apartment apartment = _context.Apartments
	            .Include(a => a.Building) // betöltjük az apartmanokhoz az épületeket
				.ThenInclude(b => b.City) // az épületekhez pedig a városokat
	            .FirstOrDefault(ap => ap.Id == apartmentId);

            RentViewModel rent = new RentViewModel { Apartment = apartment }; // létrehozunk egy új foglalást, amelynek megadjuk az apartmant

            // beállítunk egy foglalást, amely a következő megfelelő fordulónappal (minimum 1 héttel később), és egy hetes időtartammal rendelkezik
            rent.RentStartDate = DateTime.Today + TimeSpan.FromDays(7);
            while (rent.RentStartDate.DayOfWeek != apartment.Turnday)
                rent.RentStartDate += TimeSpan.FromDays(1);

            rent.RentEndDate = rent.RentStartDate + TimeSpan.FromDays(7);

            return rent;
        }
        
        /// <summary>
        /// Foglalás mentése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        /// <param name="rent">Foglalás adatai.</param>
        /// <returns>Sikeres volt-e a foglalás.</returns>
        public Boolean SaveRent(Int32? apartmentId, RentViewModel rent)
        {
            if (apartmentId == null || rent == null)
                return false;

            // ellenőrizzük az annotációkat
            if (!Validator.TryValidateObject(rent, new ValidationContext(rent, null, null), null))
                return false;

            // ellenőrizzük a dátumot
            if (_rentDateValidator.Validate(rent.RentStartDate, rent.RentEndDate, apartmentId.Value) != RentDateError.None)
                return false;
            
            // átalakítjuk az adatokat
            Guest guest = new Guest
            {
                UserName = "user" + Guid.NewGuid(), // a felhasználónevet generáljuk
                Name = rent.GuestName,
                Email = rent.GuestEmail,
                Address = rent.GuestAddress,
                PhoneNumber = rent.GuestPhoneNumber
            };
            _context.Guests.Add(guest);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                // mentéskor lehet hiba
                return false;
            }

            _context.Rents.Add(new Rent
            {
                ApartmentId = rent.Apartment.Id,
                UserId = guest.Id,
                StartDate = rent.RentStartDate,
                EndDate = rent.RentEndDate
            });

            try
            {
                _context.SaveChanges();
            }
            catch(Exception)
            {
                // mentéskor lehet hiba
                return false;
            }

            // ha idáig eljutottunk, minden sikeres volt
            return true;
        }

	    /// <summary>
	    /// Foglalás dátumainak ellenőrzése.
	    /// </summary>
	    /// <param name="start">Foglalás kezdete.</param>
	    /// <param name="end">Foglalás vége.</param>
	    /// <param name="apartmentId">Apartman azonosítója.</param>
		public RentDateError ValidateRent(DateTime start, DateTime end, int? apartmentId)
	    {
		    if (apartmentId == null)
			    return RentDateError.None;

		    return _rentDateValidator.Validate(start, end, apartmentId.Value);
	    }

	    /// <summary>
	    /// Foglalás árának lekérdezése.
	    /// </summary>
	    /// <param name="apartmentId">Apartman azonosítója.</param>
	    /// <param name="rent">Foglalás adatai.</param>
	    public Int32 GetPrice(Int32? apartmentId, RentViewModel rent)
	    {
		    if (apartmentId == null || rent == null || rent.Apartment == null)
			    return 0;

		    return rent.Apartment.Price * Convert.ToInt32((rent.RentEndDate - rent.RentStartDate).TotalDays);
	    }
	}
}