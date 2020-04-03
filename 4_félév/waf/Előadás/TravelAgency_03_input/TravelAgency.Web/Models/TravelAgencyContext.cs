using Microsoft.EntityFrameworkCore;

namespace ELTE.TravelAgency.Web.Models
{
	public class TravelAgencyContext : DbContext
	{
		public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options)
			: base(options)
		{
		}

		public DbSet<Apartment> Apartments { get; set; }
		public DbSet<Building> Buildings { get; set; }
		public DbSet<BuildingImage> BuildingImages { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Rent> Rents { get; set; }
		public DbSet<Guest> Guests { get; set; }
	}
}
