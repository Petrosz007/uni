using System;
using System.ComponentModel.DataAnnotations;

namespace ELTE.TravelAgency.Web.Models
{
    /// <summary>
    /// Foglalással kapcsolatos információk.
    /// </summary>
    public class RentViewModel
    {
        /// <summary>
        /// Vendég neve.
        /// </summary>
        [Required(ErrorMessage = "A név megadása kötelező.")] // feltételek a validáláshoz
        [StringLength(60, ErrorMessage = "A foglaló neve maximum 60 karakter lehet.")]
        public String GuestName { get; set; }

        /// <summary>
        /// Vendég e-mail címe.
        /// </summary>
        [Required(ErrorMessage = "E-mail cím megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Az e-mail cím nem megfelelő formátumú.")]
        [DataType(DataType.EmailAddress)] // pontosítjuk az adatok típusát
        public String GuestEmail { get; set; }

        /// <summary>
        /// Vendég címe.
        /// </summary>
        [Required(ErrorMessage = "A cím megadása kötelező.")]
        public String GuestAddress { get; set; }

        /// <summary>
        /// Vendég telefonszáma.
        /// </summary>
        [Required(ErrorMessage = "A telefonszám megadása kötelező.")]
        [Phone(ErrorMessage = "A telefonszám formátuma nem megfelelő.")]
        [DataType(DataType.PhoneNumber)]
        public String GuestPhoneNumber { get; set; }

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