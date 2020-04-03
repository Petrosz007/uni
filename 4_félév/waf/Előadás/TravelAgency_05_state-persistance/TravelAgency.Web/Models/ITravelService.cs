using System;
using System.Collections.Generic;

namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Az utazással kapcsolatos szolgáltatások felülete.
    /// </summary>
    public interface ITravelService
    {
        /// <summary>
        /// Városok lekérdezése.
        /// </summary>
        IEnumerable<City> Cities { get; }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        IEnumerable<Building> Buildings { get; }

        /// <summary>
        /// Épület lekérdezése.
        /// </summary>
        /// <param name="cityId">Épület azonosítója.</param>
        Building GetBuilding(Int32? cityId);

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        /// <param name="cityId">Város azonosítója.</param>
        IEnumerable<Building> GetBuildings(Int32? cityId);

        /// <summary>
        /// Épület lekérdezése apartmanokkal.
        /// </summary>
        /// <param name="cityId">Épület azonosítója.</param>
        Building GetBuildingWithApartments(Int32? cityId);

        /// <summary>
        /// Épület képek azonosítóinak lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        IEnumerable<Int32> GetBuildingImageIds(Int32? buildingId);

        /// <summary>
        /// Épület főképének lekérdezése.
        /// </summary>
        /// <param name="buildingId">Épület azonosítója.</param>
        Byte[] GetBuildingMainImage(Int32? buildingId);

        /// <summary>
        /// Épület képének lekérdezése.
        /// </summary>
        /// <param name="imageId">Kép azonosítója.</param>
        /// <param name="large">Nagy kép letöltése.</param>
        Byte[] GetBuildingImage(Int32? imageId, Boolean large);

        /// <summary>
        /// Apartman lekérdezése.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        Apartment GetApartment(Int32? apartmentId);

        /// <summary>
        /// Foglalás létrehozása.
        /// </summary>
        /// <param name="apartmentId">Apartman azonosítója.</param>
        RentViewModel NewRent(Int32? apartmentId);

		/// <summary>
		/// Foglalás mentése.
		/// </summary>
		/// <param name="apartmentId">Apartman azonosítója.</param>
		/// <param name="userName">Felhasználónév.</param>
		/// <param name="rent">Foglalás adatai.</param>
		/// <returns>Sikeres volt-e a mentés.</returns>
		Boolean SaveRent(Int32? apartmentId, String userName, RentViewModel rent);

		/// <summary>
		/// Foglalás dátumainak ellenőrzése.
		/// </summary>
		/// <param name="start">Foglalás kezdete.</param>
		/// <param name="end">Foglalás vége.</param>
		/// <param name="apartmentId">Apartman azonosítója.</param>
		RentDateError ValidateRent(DateTime start, DateTime end, Int32? apartmentId);

	    /// <summary>
	    /// Foglalás árának lekérdezése.
	    /// </summary>
	    /// <param name="apartmentId">Apartman azonosítója.</param>
	    /// <param name="rent">Foglalás adatai.</param>
	    Int32 GetPrice(Int32? apartmentId, RentViewModel rent);

	}
}
