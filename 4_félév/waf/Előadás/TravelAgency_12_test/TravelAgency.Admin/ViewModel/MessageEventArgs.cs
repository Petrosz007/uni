using System;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Üzenet eseményargumentum típusa.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Üzenet lekérdezése, vagy beállítása.
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// Üzenet eseményargumentum példányosítása.
        /// </summary>
        /// <param name="message">Üzenet.</param>
        public MessageEventArgs(String message) { Message = message; }
    }

}
