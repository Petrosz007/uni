using ELTE.TravelAgency.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Foglalással kapcsolatos információk.
    /// </summary>
    public class RentViewModel : GuestViewModel
    {
        /// <summary>
        /// Apartman.
        /// </summary>
        public Apartment Apartment { get; set; }

        /// <summary>
        /// Foglalás kezdete.
        /// </summary>
        [Required(ErrorMessage = "A kezdő dátum megadása kötelező.")]
        [DataType(DataType.Date)]
        public DateTime RentStartDate { get; set; }

        /// <summary>
        /// Foglalás vége
        /// </summary>
        [Required(ErrorMessage = "A vége dátum megadása kötelező.")]
        [DataType(DataType.Date)]
        public DateTime RentEndDate { get; set; }

        /// <summary>
        /// Teljes ár.
        /// </summary>
        [DataType(DataType.Currency)]
        public Int32 TotalPrice { get; set; }
    }
}