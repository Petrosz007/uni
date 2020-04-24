using System;

namespace ELTE.TravelAgency.Admin.Model
{
    /// <summary>
    /// Modell kivétel típusa.
    /// </summary>
    public class ModelException : Exception
    {
        public ModelException() { }

        public ModelException(Exception innerException) : base(String.Empty, innerException) { }
    }
}
