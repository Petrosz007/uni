using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Admin.Persistence
{
    /// <summary>
    /// PErziszetencia felülete.
    /// </summary>
    public interface ITravelAgencyPersistence
    {
        /// <summary>
        /// Épületek olvasása.
        /// </summary>
        Task<IEnumerable<BuildingDTO>> ReadBuildingsAsync();

        /// <summary>
        /// Városok olvasása.
        /// </summary>
        Task<IEnumerable<CityDTO>> ReadCitiesAsync();

        /// <summary>
        /// Épület létrehozása.
        /// </summary>
        /// <param name="building">Épület.</param>
        Task<Boolean> CreateBuildingAsync(BuildingDTO building);

        /// <summary>
        /// Épület módosítása.
        /// </summary>
        /// <param name="building">Épület.</param>
        Task<Boolean> UpdateBuildingAsync(BuildingDTO building);

        /// <summary>
        /// Épület törlése.
        /// </summary>
        /// <param name="building">Épület.</param>
        Task<Boolean> DeleteBuildingAsync(BuildingDTO building);

        /// <summary>
        /// Épületkép létrehozása.
        /// </summary>
        /// <param name="image">Épületkép.</param>
        Task<Boolean> CreateBuildingImageAsync(ImageDTO image);

        /// <summary>
        /// Épületkép törlése.
        /// </summary>
        /// <param name="image">Épületkép.</param>
        Task<Boolean> DeleteBuildingImageAsync(ImageDTO image);

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
