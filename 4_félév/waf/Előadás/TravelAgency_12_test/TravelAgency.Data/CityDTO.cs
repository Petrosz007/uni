using System;

namespace ELTE.TravelAgency.Data
{
    /// <summary>
    /// Város típusa.
    /// </summary>
    public class CityDTO
    {
        /// <summary>
        /// Város azonosítója.
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Város neve.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Egyenlőségvizsgálat.
        /// </summary>
        public override Boolean Equals(Object obj)
        {
            return (obj is CityDTO dto) && 
                   Id == dto.Id && 
                   Name == dto.Name; 
        }

        /// <summary>
        /// Hash-kulcs generálás.
        /// </summary>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Szöveggé alakítás.
        /// </summary>
        public override String ToString()
        {
            return Name;
        }
    }
}
