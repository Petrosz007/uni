using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Admin.Model
{
    /// <summary>
    /// Utazási ügynökség modell felülete.
    /// </summary>
    public interface ITravelAgencyModel
    {
        /// <summary>
        /// Városok lekérdezése.
        /// </summary>
        IReadOnlyList<CityDTO> Cities { get; }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        IReadOnlyList<BuildingDTO> Buildings { get; }

        /// <summary>
        /// Bejelentkezettség lekérdezése.
        /// </summary>
        Boolean IsUserLoggedIn { get; }

        /// <summary>
        /// Épületváltozás eseménye.
        /// </summary>
        event EventHandler<BuildingEventArgs> BuildingChanged;

        /// <summary>
        /// Épület létrehozása.
        /// </summary>
        /// <param name="building">Az épület.</param>
        void CreateBuilding(BuildingDTO building);

        /// <summary>
        /// Kép létrehozása.
        /// </summary>
        /// <param name="buildingId">Épület azonosító.</param>
        /// <param name="imageSmall">Kis kép.</param>
        /// <param name="imageLarge">Nagy kép.</param>
        void CreateImage(Int32 buildingId, Byte[] imageSmall, Byte[] imageLarge);

        /// <summary>
        /// Épület módosítása.
        /// </summary>
        /// <param name="building">Az épület.</param>
        void UpdateBuilding(BuildingDTO building);

        /// <summary>
        /// Épület törlése.
        /// </summary>
        /// <param name="building">Az épület.</param>
        void DeleteBuilding(BuildingDTO building);

        /// <summary>
        /// Kép törlése.
        /// </summary>
        /// <param name="image">A kép.</param>
        void DeleteImage(ImageDTO image);

        /// <summary>
        /// Adatok betöltése.
        /// </summary>
        Task LoadAsync();

        /// <summary>
        /// Adatok mentése.
        /// </summary>
        Task SaveAsync();

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="userName">Felhasználónév.</param>
        /// <param name="userPassword">Jelszó.</param>
        Task<Boolean> LoginAsync(String userName, String userPassword);

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        Task<Boolean> LogoutAsync();
    }
}
