using System;
using System.ComponentModel.DataAnnotations;

namespace ELTE.TravelAgency.Web.Models
{
	public class Apartment
	{
		public int Id { get; set; }
		public int BuildingId { get; set; }
		public int Room { get; set; }
		[UIHint("DayOfWeekDisplay")] // megadjuk a megjelenítés módját
		public DayOfWeek Turnday { get; set; }
		public string Comment { get; set; }
		public int Price { get; set; }

		public Building Building { get; set; }
	}
}