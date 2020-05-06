using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ELTE.TravelAgency.Data;

namespace ELTE.TravelAgency.Service.Models
{
	public class Building
	{
		public Building()
		{
			Apartments = new HashSet<Apartment>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public int CityId { get; set; }
		public int SeaDistance { get; set; }
        public ShoreType Shore { get; set; }
        public Feature Features { get; set; }
        public double LocationX { get; set; }
		public double LocationY { get; set; }
		public string Comment { get; set; }
		

		public ICollection<Apartment> Apartments { get; set; }
		public City City { get; set; }
	}
}
