using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Admin.Persistence
{
    /// <summary>
    /// Szolgáltatás alapú perzisztencia.
    /// </summary>
    public class TravelAgencyServicePersistence : ITravelAgencyPersistence
    {
        private HttpClient _client;

        /// <summary>
        /// Szolgáltatás alkapú perszisztencia példányosítása.
        /// </summary>
        /// <param name="baseAddress">A szolgáltatás címe.</param>
        public TravelAgencyServicePersistence(String baseAddress)
        {
            _client = new HttpClient(); // a szolgáltatás kliense
            _client.BaseAddress = new Uri(baseAddress); // megadjuk neki a címet
        }

        /// <summary>
        /// Épületek betöltése.
        /// </summary>
        public async Task<IEnumerable<BuildingDTO>> ReadBuildingsAsync()
        {
            try
            {
                // a kéréseket a kliensen keresztül végezzük
                HttpResponseMessage response = await _client.GetAsync("api/buildings/");
                if (response.IsSuccessStatusCode) // amennyiben sikeres a művelet
                {
                    IEnumerable<BuildingDTO> buildings = await response.Content.ReadAsAsync<IEnumerable<BuildingDTO>>();
                    // a tartalmat JSON formátumból objektumokká alakítjuk
                    
                    return buildings;
                }
                else
                {
                    throw new PersistenceUnavailableException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }

        }
        
        /// <summary>
        /// Épület létrehozása.
        /// </summary>
        /// <param name="building">Épület.</param>
        public async Task<Boolean> CreateBuildingAsync(BuildingDTO building)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/buildings/", building); // az értékeket azonnal JSON formátumra alakítjuk
                building.Id = (await response.Content.ReadAsAsync<BuildingDTO>()).Id; // a válaszüzenetben megkapjuk a végleges azonosítót
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        /// <summary>
        /// Épület módosítása.
        /// </summary>
        /// <param name="building">Épület.</param>
        public async Task<Boolean> UpdateBuildingAsync(BuildingDTO building)
        {
            try
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync("api/buildings/", building);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        /// <summary>
        /// Épület törlése.
        /// </summary>
        /// <param name="building">Épület.</param>
        public async Task<Boolean> DeleteBuildingAsync(BuildingDTO building)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync("api/buildings/" + building.Id);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }
    }
}
