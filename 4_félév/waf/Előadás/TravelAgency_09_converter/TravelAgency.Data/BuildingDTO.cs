using System;

namespace ELTE.TravelAgency.Data
{
    /// <summary>
    ///Tengerpart típusa.
    /// </summary>
    public enum ShoreType
    {
        /// <summary>
        /// Homokos
        /// </summary>
        Sandy,
        /// <summary>
        /// Kavicsos
        /// </summary>
        Gravelly,
        /// <summary>
        /// Sziklás
        /// </summary>
        Rocky
    }

    [Flags]
    public enum Feature
    {
        /// <summary>
        /// Semmi
        /// </summary>
        None = 0,
        /// <summary>
        /// Főút
        /// </summary>
        MainRoad = 1,
        /// <summary>
        /// Parti szolgálat
        /// </summary>
        CoastService = 2,
        /// <summary>
        /// Úszómedence
        /// </summary>
        SwimmingPool = 4,
        /// <summary>
        /// Kert
        /// </summary>
        Garden = 8,
        /// <summary>
        /// Saját parkolói
        /// </summary>
        PrivateParking = 16
    }

	/// <summary>
	/// Épület típusa.
	/// </summary>
	public class BuildingDTO
    {
        /// <summary>
        /// Épület azonosítója.
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Épület neve.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Város.
        /// </summary>
        public CityDTO City { get; set; }

        /// <summary>
        /// Tengerpart távolság.
        /// </summary>
        public Int32 SeaDistance { get; set; }
        
        /// <summary>
        /// Tengerpart típusa.
        /// </summary>
        public ShoreType Shore { get; set; }

        /// <summary>
        /// Jellemzők.
        /// </summary>
        public FeatureDTO[] Features { get; set; }

        /// <summary>
        /// X pozíció.
        /// </summary>
        public Double LocationX { get; set; }
        
        /// <summary>
        /// Y pozíció.
        /// </summary>
        public Double LocationY { get; set; }
        
        /// <summary>
        /// Megjegyzés.
        /// </summary>
        public String Comment { get; set; }
    }
}
