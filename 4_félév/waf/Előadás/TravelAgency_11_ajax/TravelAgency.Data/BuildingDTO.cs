using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Képek.
        /// </summary>
        public IList<ImageDTO> Images { get; set; }

        /// <summary>
        /// Új <see cref="BuildingDTO"/> objektum példányosítása.
        /// </summary>
        public BuildingDTO()
        {
            Features = new FeatureDTO[Enum.GetNames(typeof(Feature)).Length - 1];
            for (int i = 0; i < Features.Length; ++i)
            {
                Features[i] = new FeatureDTO {Id = i};
            }

            Images = new List<ImageDTO>();
        }

    }
}
