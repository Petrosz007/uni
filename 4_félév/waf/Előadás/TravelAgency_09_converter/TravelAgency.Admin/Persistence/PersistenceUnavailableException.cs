using System;

namespace ELTE.TravelAgency.Admin.Persistence
{
    /// <summary>
    /// Perzisztencia elérhetetlenség kivétel típusa.
    /// </summary>
    public class PersistenceUnavailableException : Exception
    {
        public PersistenceUnavailableException(String message) : base(message) { }

        public PersistenceUnavailableException(Exception innerException) : base("Exception occurred.", innerException) { }
    }
}
