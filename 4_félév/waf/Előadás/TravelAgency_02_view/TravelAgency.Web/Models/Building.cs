using System;
using System.Collections.Generic;

namespace ELTE.TravelAgency.Web.Models
{
	/// <summary>
	///Tengerpart típusa.
	/// </summary>
	public enum ShoreType
	{
		/// <summary>
		/// Homokos
		/// </summary>
		Sandy,
		/// <summary>
		/// Kavicsos
		/// </summary>
		Gravelly,
		/// <summary>
		/// Sziklás
		/// </summary>
		Rocky
	}

	[Flags]
	public enum Feature
	{
		/// <summary>
		/// Semmi
		/// </summary>
		None = 0,
		/// <summary>
		/// Főút
		/// </summary>
		MainRoad = 1,
		/// <summary>
		/// Parti szolgálat
		/// </summary>
		CoastService = 2,
		/// <summary>
		/// Úszómedence
		/// </summary>
		SwimmingPool = 4,
		/// <summary>
		/// Kert
		/// </summary>
		Garden = 8,
		/// <summary>
		/// Saját parkolói
		/// </summary>
		PrivateParking = 16
	}

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
