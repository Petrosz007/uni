using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Admin.Persistence
{
    /// <summary>
    /// Perziszetencia felülete.
    /// </summary>
    public interface ITravelAgencyPersistence
    {
        /// <summary>
        /// Épületek olvasása.
        /// </summary>
        Task<IEnumerable<BuildingDTO>> ReadBuildingsAsync();
        
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
    }
}
