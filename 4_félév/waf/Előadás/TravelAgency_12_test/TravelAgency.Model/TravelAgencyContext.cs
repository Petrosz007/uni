using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELTE.TravelAgency.Model
{
	public class TravelAgencyContext : IdentityDbContext<Guest, IdentityRole<int>, int>
	{
        public TravelAgencyContext() // Mockoláshoz szükséges
        { }

		public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Guest>().ToTable("Guests");
			// A felhasználói tábla alapértelmezett neve AspNetUsers lenne az adatbázisban, de ezt felüldefiniálhatjuk.
		}

		public virtual DbSet<Apartment> Apartments { get; set; }
		public virtual DbSet<Building> Buildings { get; set; }
		public virtual DbSet<BuildingImage> BuildingImages { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<Rent> Rents { get; set; }

		/*
		 * A IdentityDbContext típustól további, az authentikációhoz és autorizációhoz kapcsolódó kollekciókat öröklünk, pl.:
		 * Users
		 * UserRoles
		 * stb.
		 */
	}
}
