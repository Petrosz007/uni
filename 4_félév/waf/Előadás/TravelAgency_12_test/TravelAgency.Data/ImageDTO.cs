using System;

namespace ELTE.TravelAgency.Data
{
    public class ImageDTO
    {
        /// <summary>
        /// Azonosító lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Épület azonosító lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 BuildingId { get; set; }

        /// <summary>
        /// Kis kép lekérdezése, vagy beállítása.
        /// </summary>
        public Byte[] ImageSmall { get; set; }

        /// <summary>
        /// Nagy kép lekérdezése, vagy beállítása.
        /// </summary>
        public Byte[] ImageLarge { get; set; }
    }
}
