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

                    // képek lekérdezése:
                    foreach (BuildingDTO building in buildings)
                    {
                        response = await _client.GetAsync("api/images/" + building.Id);
                        if (response.IsSuccessStatusCode)
                        {
                            building.Images = (await response.Content.ReadAsAsync<IEnumerable<ImageDTO>>()).ToList();
                        }
                    }

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
        /// Városok betöltése.
        /// </summary>
        public async Task<IEnumerable<CityDTO>> ReadCitiesAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/cities/");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<CityDTO>>();
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

        /// <summary>
        /// Épületkép létrehozása.
        /// </summary>
        /// <param name="image">Épületkép.</param>
        public async Task<Boolean> CreateBuildingImageAsync(ImageDTO image)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/images/", image); // elküldjük a képet
                if (response.IsSuccessStatusCode)
                {
                    image.Id = await response.Content.ReadAsAsync<Int32>(); // a válaszüzenetben megkapjuk az azonosítót
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        /// <summary>
        /// Épületkép törlése.
        /// </summary>
        /// <param name="image">Épületkép.</param>
        public async Task<Boolean> DeleteBuildingImageAsync(ImageDTO image)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync("api/images/" + image.Id);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }


        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="userName">Felhasználónév.</param>
        /// <param name="userPassword">Jelszó.</param>
        public async Task<Boolean> LoginAsync(String userName, String userPassword)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/account/login/" + userName + "/" + userPassword);
                return response.IsSuccessStatusCode; // a művelet eredménye megadja a bejelentkezés sikerességét
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        public async Task<Boolean> LogoutAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/account/logout");
                return !response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new PersistenceUnavailableException(ex);
            }
        }
    }
}
