namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Foglalási dátum hiba felsorolási típusa.
    /// </summary>
    public enum RentDateError
    {
        /// <summary>
        /// Nincs hiba.
        /// </summary>
        None,

        /// <summary>
        /// Hibás kezdődátum.
        /// </summary>
        StartInvalid,

        /// <summary>
        /// Hibás vége dátum.
        /// </summary>
        EndInvalid,

        /// <summary>
        /// Hibás hossz.
        /// </summary>
        LengthInvalid,

        /// <summary>
        /// Ütközés.
        /// </summary>
        Conflicting
    }
}