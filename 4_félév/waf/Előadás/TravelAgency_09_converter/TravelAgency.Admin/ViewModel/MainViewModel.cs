using ELTE.TravelAgency.Admin.Model;
using ELTE.TravelAgency.Admin.Persistence;
using ELTE.TravelAgency.Data;
using System;
using System.Collections.ObjectModel;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Nézetmodell típusa.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ITravelAgencyModel _model;
        private ObservableCollection<BuildingDTO> _buildings;
        private ObservableCollection<CityDTO> _cities;
        private BuildingDTO _currentBuilding;
        private Boolean _isLoaded;
        private Int32 _selectedIndex;

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
        /// Új épület lekérdezése.
        /// </summary>
        public BuildingDTO CurrentBuilding
        { 
            get { return _currentBuilding; } 
            private set
            {
                if (_currentBuilding != value)
                {
                    _currentBuilding = value;
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
        /// A kiválasztott elem indexének lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 SelectedIndex
        {
            get { return _selectedIndex; }
            set 
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged();

                    // létrehozunk egy új aktuális épületet, amibe bemásoljuk a kijelölt épület adatait
                    if (_selectedIndex >= 0 && _selectedIndex < _buildings.Count)
                        CurrentBuilding = new BuildingDTO
                        {
                            Id = _buildings[_selectedIndex].Id,
                            Name = _buildings[_selectedIndex].Name,
                            City = _buildings[_selectedIndex].City,
                            SeaDistance = _buildings[_selectedIndex].SeaDistance,
                            Shore = _buildings[_selectedIndex].Shore,
                            Features = _buildings[_selectedIndex].Features,
                            Comment = _buildings[_selectedIndex].Comment,
                            LocationX = _buildings[_selectedIndex].LocationX,
                            LocationY = _buildings[_selectedIndex].LocationY
                        };
                }
            }
        }

        /// <summary>
        /// Hozzáadás parancsának lekérdezése.
        /// </summary>
        public DelegateCommand CreateBuildingCommand { get; private set; }

        /// <summary>
        /// Törlés parancsának lekérdezése.
        /// </summary>
        public DelegateCommand DeleteBuildingCommand { get; private set; }

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
        /// Módosítás parancsának lekérdezése.
        /// </summary>
        public DelegateCommand UpdateBuildingCommand { get; private set; }
        
        /// <summary>
        /// Alkalmazásból való kilépés eseménye.
        /// </summary>
        public event EventHandler ExitApplication;

        /// <summary>
        /// Nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell.</param>
        public MainViewModel(ITravelAgencyModel model)
        {
            _model = model;
            _isLoaded = false;
            _selectedIndex = -1;
            _currentBuilding = new BuildingDTO();

            CreateBuildingCommand = new DelegateCommand(param =>
            {
                _model.CreateBuilding(CurrentBuilding);
                Buildings.Add(CurrentBuilding);
                CurrentBuilding = new BuildingDTO();
            });

            DeleteBuildingCommand = new DelegateCommand(param =>
            {
                _model.DeleteBuilding(CurrentBuilding);
                Buildings.RemoveAt(SelectedIndex);
            });
            
            UpdateBuildingCommand = new DelegateCommand(param =>
            {
                Int32 index = SelectedIndex; // a kiválasztott sornak megfelelően végezzük a módosítást

                Buildings.RemoveAt(index);
                _model.UpdateBuilding(CurrentBuilding);
                Buildings.Insert(index, _model.Buildings[index]);
            }
            );

            LoadCommand = new DelegateCommand(param => LoadAsync());
            SaveCommand = new DelegateCommand(param => SaveAsync());

            ExitCommand = new DelegateCommand(param => OnExitApplication());
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
                SelectedIndex = -1;
                IsLoaded = true;
            }
            catch (PersistenceUnavailableException)
            {
                OnMessageApplication("A betöltés sikertelen! Nincs kapcsolat a kiszolgálóval.");
            }
        }

        /// <summary>
        /// Mentés végrehajtása.
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
        /// Alkalmazásból való kilépés eseménykiváltása.
        /// </summary>
        private void OnExitApplication()
        {
            if (ExitApplication != null)
                ExitApplication(this, EventArgs.Empty);
        }
    }
}
