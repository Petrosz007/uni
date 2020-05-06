using System;

namespace ELTE.TravelAgency.Admin.Model
{
    /// <summary>
    /// Épület eseményargumentum típusa.
    /// </summary>
    public class BuildingEventArgs : EventArgs
    {
        /// <summary>
        /// Épület azonosító lekérdezése, vagy beállítása.
        /// </summary>
        public Int32 BuildingId { get; set; }
    }
}
