using ELTE.TravelAgency.Admin.Model;
using ELTE.TravelAgency.Admin.Persistence;
using ELTE.TravelAgency.Data;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// A nézetmodell típusa.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ITravelAgencyModel _model;
        private ObservableCollection<BuildingDTO> _buildings;
        private ObservableCollection<CityDTO> _cities;
        private BuildingDTO _selectedBuilding;
        private Boolean _isLoaded;

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        public ObservableCollection<BuildingDTO> Buildings
        {
            get { return _buildings; }
            private set
            {
                if (_buildings != value)
                {
                    _buildings = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Épületek lekérdezése.
        /// </summary>
        public ObservableCollection<CityDTO> Cities
        {
            get { return _cities; }
            private set
            {
                if (_cities != value)
                {
                    _cities = value;
                    OnPropertyChanged();
                }
            }
        }
        
        /// <summary>
        /// Betöltöttség lekérdezése.
        /// </summary>
        public Boolean IsLoaded
        {
            get { return _isLoaded; }
            private set
            {
                if (_isLoaded != value)
                {
                    _isLoaded = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Kijelölt épület lekérdezése, vagy beállítása.
        /// </summary>
        public BuildingDTO SelectedBuilding 
        { 
            get { return _selectedBuilding; }
            set
            {
                if (_selectedBuilding != value)
                {
                    _selectedBuilding = value;
                    OnPropertyChanged();
                }
            } 
        }

        /// <summary>
        /// Szerkesztett épület lekérdezése.
        /// </summary>
        public BuildingDTO EditedBuilding { get; private set; }

        /// <summary>
        /// Épület törlés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand CreateBuildingCommand { get; private set; }

        /// <summary>
        /// Kép hozzáadás parancsának lekérdezése.
        /// </summary>
        public DelegateCommand CreateImageCommand { get; private set; }

        /// <summary>
        /// Módosítás parancsának lekérdezése.
        /// </summary>
        public DelegateCommand UpdateBuildingCommand { get; private set; }

        /// <summary>
        /// Épület törlés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand DeleteBuildingCommand { get; private set; }

        /// <summary>
        /// Kép törlés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand DeleteImageCommand { get; private set; }

        /// <summary>
        /// Változtatások mentése parancsának lekérdezése.
        /// </summary>
        public DelegateCommand SaveChangesCommand { get; private set; }

        /// <summary>
        /// Változtatások elvetése parancsának lekérdezése.
        /// </summary>
        public DelegateCommand CancelChangesCommand { get; private set; }

        /// <summary>
        /// Kilépés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand ExitCommand { get; private set; }

        /// <summary>
        /// Betöltés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand LoadCommand { get; private set; }

        /// <summary>
        /// Mentés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand SaveCommand { get; private set; }
        
        /// <summary>
        /// Alkalmazásból való kilépés eseménye.
        /// </summary>
        public event EventHandler ExitApplication;

        /// <summary>
        /// Épület szerkesztés elindításának eseménye.
        /// </summary>
        public event EventHandler BuildingEditingStarted;

        /// <summary>
        /// Épület szerkesztés befejeztének eseménye.
        /// </summary>
        public event EventHandler BuildingEditingFinished;

        /// <summary>
        /// Képszerkesztés elindításának eseménye.
        /// </summary>
        public event EventHandler<BuildingEventArgs> ImageEditingStarted;

        /// <summary>
        /// Nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell.</param>
        public MainViewModel(ITravelAgencyModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;
            _model.BuildingChanged += Model_BuildingChanged;
            _isLoaded = false;

            CreateBuildingCommand = new DelegateCommand(param =>
            {
                EditedBuilding = new BuildingDTO();  // a szerkesztett épület új lesz
                OnBuildingEditingStarted();
            });
            CreateImageCommand = new DelegateCommand(param => OnImageEditingStarted((param as BuildingDTO).Id));
            UpdateBuildingCommand = new DelegateCommand(param => UpdateBuilding(param as BuildingDTO));
            DeleteBuildingCommand = new DelegateCommand(param => DeleteBuilding(param as BuildingDTO));
            DeleteImageCommand = new DelegateCommand(param => DeleteImage(param as ImageDTO));
            SaveChangesCommand = new DelegateCommand(param => SaveChanges());
            CancelChangesCommand = new DelegateCommand(param => CancelChanges());
            LoadCommand = new DelegateCommand(param => LoadAsync());
            SaveCommand = new DelegateCommand(param => SaveAsync());
            ExitCommand = new DelegateCommand(param => OnExitApplication());
        }

        /// <summary>
        /// Épület frissítése.
        /// </summary>
        /// <param name="building">Az épület.</param>
        private void UpdateBuilding(BuildingDTO building)
        {
            if (building == null)
                return;

            EditedBuilding = new BuildingDTO
            {
                Id = building.Id,
                Name = building.Name,
                City = building.City,
                SeaDistance = building.SeaDistance,
                Shore = building.Shore,
                Features = building.Features.Select(feature => new FeatureDTO
                {
                    Id = feature.Id,
                    IsAvailable = feature.IsAvailable
                }).ToArray(),
                LocationX = building.LocationX,
                LocationY = building.LocationY,
                Comment = building.Comment
            }; // a szerkesztett épület adatait áttöltjük a kijelöltből

            OnBuildingEditingStarted();
        }

        /// <summary>
        /// Épület törlése.
        /// </summary>
        /// <param name="building">Az épület.</param>
        private void DeleteBuilding(BuildingDTO building)
        {
            if (building == null || !Buildings.Contains(building))
                return;

            Buildings.Remove(building);

            _model.DeleteBuilding(building);
        }

        /// <summary>
        /// Kép törlése.
        /// </summary>
        /// <param name="image">A kép.</param>
        private void DeleteImage(ImageDTO image)
        {
            if (image == null)
                return;

            _model.DeleteImage(image);
        }

        /// <summary>
        /// Változtatások mentése.
        /// </summary>
        private void SaveChanges()
        {
            // ellenőrzések
            if (String.IsNullOrEmpty(EditedBuilding.Name))
            {
                OnMessageApplication("Az épületnév nincs megadva!");
                return;
            }
            if (EditedBuilding.City == null)
            {
                OnMessageApplication("A város nincs megadva!");
                return;
            }

            // mentés
            if (EditedBuilding.Id == 0) // ha új az épület
            {
                _model.CreateBuilding(EditedBuilding);
                Buildings.Add(EditedBuilding);
                SelectedBuilding = EditedBuilding;
            }
            else // ha már létezik az épület
            {
                _model.UpdateBuilding(EditedBuilding);
            }

            EditedBuilding = null;

            OnBuildingEditingFinished();
        }

        /// <summary>
        /// Változtatások elvetése.
        /// </summary>
        private void CancelChanges()
        {
            EditedBuilding = null;
            OnBuildingEditingFinished();
        }

        /// <summary>
        /// Betöltés végrehajtása.
        /// </summary>
        private async void LoadAsync()
        {
            try
            {
                await _model.LoadAsync();
                Buildings = new ObservableCollection<BuildingDTO>(_model.Buildings); // az adatokat egy követett gyűjteménybe helyezzük
                Cities = new ObservableCollection<CityDTO>(_model.Cities);
                IsLoaded = true;
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A betöltés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        /// <summary>
        /// Mentés végreahajtása.
        /// </summary>
        private async void SaveAsync()
        {
            try
            {
                await _model.SaveAsync();
                OnMessageApplication("A mentés sikeres!");
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A mentés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        /// <summary>
        /// Épület megváltozásának eseménykezelése.
        /// </summary>
        private void Model_BuildingChanged(object sender, BuildingEventArgs e)
        {
            Int32 index = Buildings.IndexOf(Buildings.FirstOrDefault(building => building.Id == e.BuildingId));
            Buildings.RemoveAt(index); // módosítjuk a kollekciót
            Buildings.Insert(index, _model.Buildings[index]);

            SelectedBuilding = Buildings[index]; // és az aktuális épületet
        }
        
        /// <summary>
        /// Alkalmazásból való kilépés eseménykiváltása.
        /// </summary>
        private void OnExitApplication()
        {
            if (ExitApplication != null)
                ExitApplication(this, EventArgs.Empty);
        }

        /// <summary>
        /// Épület szerkesztés elindításának eseménykiváltása.
        /// </summary>
        private void OnBuildingEditingStarted()
        {
            if (BuildingEditingStarted != null)
                BuildingEditingStarted(this, EventArgs.Empty);
        }

        /// <summary>
        /// Épület szerkesztés befejeztének eseménykiváltása.
        /// </summary>
        private void OnBuildingEditingFinished()
        {
            if (BuildingEditingFinished != null)
                BuildingEditingFinished(this, EventArgs.Empty);
        }

        /// <summary>
        /// Képszerkesztés elindításának eseménykiváltása.
        /// </summary>
        /// <param name="buildingId">Épület azonosító.</param>
        private void OnImageEditingStarted(Int32 buildingId)
        {
            if (ImageEditingStarted != null)
                ImageEditingStarted(this, new BuildingEventArgs { BuildingId = buildingId });
        }
    }
}
