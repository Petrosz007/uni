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
        /// Épület hozzáadása.
        /// </summary>
        /// <param name="building">Az épület.</param>
        void CreateBuilding(BuildingDTO building);

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
        /// Adatok betöltése.
        /// </summary>
        Task LoadAsync();

        /// <summary>
        /// Adatok mentése.
        /// </summary>
        Task SaveAsync();
    }
}
