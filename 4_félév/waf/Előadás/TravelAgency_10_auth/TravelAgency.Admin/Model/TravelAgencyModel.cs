using ELTE.TravelAgency.Admin.Persistence;
using ELTE.TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELTE.TravelAgency.Admin.Model
{
    /// <summary>
    /// Utazási ügynökség modell megvalósítása.
    /// </summary>
    public class TravelAgencyModel : ITravelAgencyModel
    {
        /// <summary>
        /// Adat állapotjelzések.
        /// </summary>
        private enum DataFlag
        {
            Create,
            Update,
            Delete
        }

        private ITravelAgencyPersistence _persistence;
        private List<BuildingDTO> _buildings;
        private Dictionary<BuildingDTO, DataFlag> _buildingFlags;
        private Dictionary<ImageDTO, DataFlag> _imageFlags;
        private List<CityDTO> _cities;

        /// <summary>
        /// Modell példányosítása.
        /// </summary>
        /// <param name="persistence">A perzisztencia.</param>
        public TravelAgencyModel(ITravelAgencyPersistence persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException(nameof(persistence));

            IsUserLoggedIn = false;
            _persistence = persistence;
        }

        /// <summary>
        /// Városok lekérdezése.
        /// </summary>
        public IReadOnlyList<CityDTO> Cities
        {
            get { return _cities; }
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        public IReadOnlyList<BuildingDTO> Buildings
        {
            get { return _buildings; }
        }

        /// <summary>
        /// Bejelentkezettség lekérdezése.
        /// </summary>
        public Boolean IsUserLoggedIn { get; private set; }

        /// <summary>
        /// Épületváltozás eseménye.
        /// </summary>
        public event EventHandler<BuildingEventArgs> BuildingChanged;

        /// <summary>
        /// Épület hozzáadása.
        /// </summary>
        /// <param name="building">Az épület.</param>
        public void CreateBuilding(BuildingDTO building)
        {
            if (building == null)
                throw new ArgumentNullException(nameof(building));
            if (_buildings.Contains(building))
                throw new ArgumentException("The building is already in the collection.", nameof(building));

            building.Id = (_buildings.Count > 0 ? _buildings.Max(b => b.Id) : 0) + 1; // generálunk egy új, ideiglenes azonosítót (nem fog átkerülni a szerverre)
            _buildingFlags.Add(building, DataFlag.Create);
            _buildings.Add(building);
        }

        /// <summary>
        /// Kép hozzáadása.
        /// </summary>
        /// <param name="buildingId">Épület azonosító.</param>
        /// <param name="imageSmall">Kis kép.</param>
        /// <param name="imageLarge">Nagy kép.</param>
        public void CreateImage(Int32 buildingId, Byte[] imageSmall, Byte[] imageLarge)
        {
            BuildingDTO building = _buildings.FirstOrDefault(b => b.Id == buildingId);
            if (building == null)
                throw new ArgumentException("The building does not exist.", nameof(buildingId));

            // létrehozzuk a képet
            ImageDTO image = new ImageDTO 
            { 
                Id = _buildings.Max(b => b.Images.Any() ? b.Images.Max(im => im.Id) : 0) + 1,
                BuildingId = buildingId, 
                ImageSmall = imageSmall, 
                ImageLarge = imageLarge 
            };

            // hozzáadjuk
            building.Images.Add(image);
            _imageFlags.Add(image, DataFlag.Create);

            // jelezzük a változást
            OnBuildingChanged(building.Id);
        }

        /// <summary>
        /// Épület módosítása.
        /// </summary>
        /// <param name="building">Az épület.</param>
        public void UpdateBuilding(BuildingDTO building)
        { 
            if (building == null)
                throw new ArgumentNullException(nameof(building));

            // keresés azonosító alapján
            BuildingDTO buildingToModify = _buildings.FirstOrDefault(b => b.Id == building.Id);

            if (buildingToModify == null)
                throw new ArgumentException("The building does not exist.", nameof(building));

            // módosítások végrehajtása
            buildingToModify.City = building.City;
            buildingToModify.Comment = building.Comment;
            buildingToModify.Name = building.Name;
            buildingToModify.LocationX = building.LocationX;
            buildingToModify.LocationY = building.LocationY;
            buildingToModify.SeaDistance = building.SeaDistance;
            buildingToModify.Shore = building.Shore;
            buildingToModify.Features = building.Features;

            // külön állapottal jelezzük, ha egy adat újonnan hozzávett
            if (_buildingFlags.ContainsKey(buildingToModify) && _buildingFlags[buildingToModify] == DataFlag.Create)
            {
                _buildingFlags[buildingToModify] = DataFlag.Create;
            }
            else
            {
                _buildingFlags[buildingToModify] = DataFlag.Update;
            }

            // jelezzük a változást
            OnBuildingChanged(building.Id);
        }

        /// <summary>
        /// Épület törlése.
        /// </summary>
        /// <param name="building">Az épület.</param>
        public void DeleteBuilding(BuildingDTO building)
        {
            if (building == null)
                throw new ArgumentNullException(nameof(building));

            // keresés azonosító alapján
            BuildingDTO buildingToDelete = _buildings.FirstOrDefault(b => b.Id == building.Id);

            if (buildingToDelete == null)
                throw new ArgumentException("The building does not exist.", nameof(building));

            // külön kezeljük, ha egy adat újonnan hozzávett (ekkor nem kell törölni a szerverről)
            if (_buildingFlags.ContainsKey(buildingToDelete) && _buildingFlags[buildingToDelete] == DataFlag.Create)
                _buildingFlags.Remove(buildingToDelete);
            else
                _buildingFlags[buildingToDelete] = DataFlag.Delete;

            _buildings.Remove(buildingToDelete);
        }

        /// <summary>
        /// Kép törlése.
        /// </summary>
        /// <param name="image">A kép.</param>
        public void DeleteImage(ImageDTO image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            // megkeressük a képet
            foreach (BuildingDTO building in _buildings)
            {
                if (!building.Images.Contains(image))
                    continue;

                // kezeljük az állapotjelzéseket is
                if (_imageFlags.ContainsKey(image))
                    _imageFlags.Remove(image);
                else
                    _imageFlags.Add(image, DataFlag.Delete);

                // töröljük a képet
                building.Images.Remove(image);

                // jellezzük a változást
                OnBuildingChanged(building.Id);

                return;
            }

            // ha idáig eljutott, akkor nem sikerült képet törölni+
            throw new ArgumentException("The image does not exist.", nameof(image));
        }


        /// <summary>
        /// Adatok betöltése.
        /// </summary>
        public async Task LoadAsync()
        {
            // adatok
            _buildings = (await _persistence.ReadBuildingsAsync()).ToList();
            _cities = (await _persistence.ReadCitiesAsync()).ToList();

            // állapotjelzések
            _buildingFlags = new Dictionary<BuildingDTO, DataFlag>();
            _imageFlags = new Dictionary<ImageDTO, DataFlag>();
        }

        /// <summary>
        /// Adatok mentése.
        /// </summary>
        public async Task SaveAsync()
        {
            // épületek
            List<BuildingDTO> buildingsToSave = _buildingFlags.Keys.ToList();

            foreach (BuildingDTO building in buildingsToSave)
            {
                Boolean result = true;

                // az állapotjelzőnek megfelelő műveletet végezzük el
                switch (_buildingFlags[building])
                {
                    case DataFlag.Create:
                        result = await _persistence.CreateBuildingAsync(building);
                        break;
                    case DataFlag.Delete:
                        result = await _persistence.DeleteBuildingAsync(building);
                        break;
                    case DataFlag.Update:
                        result = await _persistence.UpdateBuildingAsync(building);
                        break;
                }

                if (!result)
                    throw new InvalidOperationException("Operation " + _buildingFlags[building] + " failed on building " + building.Id);
                
                // ha sikeres volt a mentés, akkor törölhetjük az állapotjelzőt
                _buildingFlags.Remove(building);
            }

            // képek
            List<ImageDTO> imagesToSave = _imageFlags.Keys.ToList();

            foreach (ImageDTO image in imagesToSave)
            {
                Boolean result = true;

                switch (_imageFlags[image])
                {
                    case DataFlag.Create:
                        result = await _persistence.CreateBuildingImageAsync(image);
                        break;
                    case DataFlag.Delete:
                        result = await _persistence.DeleteBuildingImageAsync(image);
                        break;
                }

                if (!result)
                    throw new InvalidOperationException("Operation " + _imageFlags[image] + " failed on image " + image.Id);

                // ha sikeres volt a mentés, akkor törölhetjük az állapotjelzőt
                _imageFlags.Remove(image);
            }
        }


        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="userName">Felhasználónév.</param>
        /// <param name="userPassword">Jelszó.</param>
        public async Task<Boolean> LoginAsync(String userName, String userPassword)
        {
            IsUserLoggedIn = await _persistence.LoginAsync(userName, userPassword);
            return IsUserLoggedIn;
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        public async Task<Boolean> LogoutAsync()
        {
            if (!IsUserLoggedIn)
                return true;

            IsUserLoggedIn = !(await _persistence.LogoutAsync());

            return IsUserLoggedIn;
        }

        private void OnBuildingChanged(Int32 buildingId)
        {
            if (BuildingChanged != null)
                BuildingChanged(this, new BuildingEventArgs { BuildingId = buildingId });
        }
    }
}
