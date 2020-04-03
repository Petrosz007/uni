using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELTE.TravelAgency.Web.Models
{
	/// <summary>
	/// Adatbázis inicializátor.
	/// </summary>
	public static class DbInitializer
	{
		private static TravelAgencyContext _context;

		/// <summary>
		/// Adatbázis inicializálása.
		/// </summary>
		/// <param name="serviceProvider">Szolgáltatás IOC konténer.</param>
		/// <param name="imageDirectory">Képek útvonala.</param>
		public static void Initialize(IServiceProvider serviceProvider, string imageDirectory)
		{
			_context = serviceProvider.GetRequiredService<TravelAgencyContext>();

			// Adatbázis létrehozása, amennyiben nem létezik
			//context.Database.EnsureCreated();

			// Adatbázis séma automatikus migrálása indításkor, amennyiben szükséges
			_context.Database.Migrate();

			// Apartmanok keresése
			if (_context.Apartments.Any())
			{
				return; // Az adatbázis már inicializálva van.
			}

			SeedCities();
			SeedBuildings();
			SeedApartments();
			SeedBuildingImages(imageDirectory);
		}

		/// <summary>
		/// Városok inicializálása.
		/// </summary>
		private static void SeedCities()
		{

			var cities = new City[]
			{
				new City {Name = "Cavallino"},
				new City {Name = "Lido di Jesolo"},
			};
			foreach (City c in cities)
			{
				_context.Cities.Add(c);
			}

			_context.SaveChanges();
		}

		/// <summary>
		/// Épületek inicializálása.
		/// </summary>
		private static void SeedBuildings()
		{
			var buildings = new Building[]
			{
				new Building
				{
					Name = "Petra bungaló",
					CityId = 1,
					SeaDistance = 100,
					Shore = ShoreType.Sandy,
					Features = Feature.MainRoad | Feature.SwimmingPool,
					LocationX = 45.4591,
					LocationY = 12.5068,
					Comment = "Szállás bungallóban 100m strandtól (homokos tengerpart). A területen található étterem, bár, játszótér. Éjszakai nyugalmat betartani. Ott-tartózkodás szombattól szombatig. A szállásszolgáltató beszél is csehül. Parkoló - nem őrzött, ingyenes. Busz 100m. Vonat (állomás San Dona di Piave) 25km. Repülőtér (Venezia - Marco Polo) 40km. Cavallino - központ.')",
				},
				new Building
				{
					Name = "Willerby bungaló",
					CityId = 1,
					SeaDistance = 100,
					Shore = ShoreType.Rocky,
					Features = Feature.MainRoad | Feature.Garden,
					LocationX = 45.4566,
					LocationY = 12.5004,
					Comment = "Szállás bungallóban 100m strandtól (homokos tengerpart). A területen található étterem, bár, játszótér. Éjszakai nyugalmat betartani. Ott-tartózkodás szombattól szombatig. A szállásszolgáltató beszél is csehül. Parkoló - nem őrzött, ingyenes. Busz 100m. Vonat (állomás San Dona di Piave) 25km. Repülőtér (Venezia - Marco Polo) 40km. Cavallino - központ.')",
				},
				new Building
				{
					Name = "Cavallino Hotel",
					CityId = 1,
					SeaDistance = 50,
					Shore = ShoreType.Gravelly,
					Features = Feature.MainRoad | Feature.CoastService,
					LocationX = 45.4788,
					LocationY = 12.5677,
					Comment = "Szálloda községben 50m a tengertől. A hotelban / területén található étterem, terasz, bár, kávéház, társalgó TV-vel, biliárd, darts, szauna, nyári medence, kerti pihenőhely, homokozó, hinta, légkondicionáló; illeték fejében: sportfelszerelés-kölcsönző, lovaglás, pénzváltó. Internet-csatlakozás. Napernyő és napozóágy a szállás árában. Parkoló - nem őrzött, ingyenes. Busz 20m. Repülőtér (Aeroporto Marco Polo Venezia) 30km. Cavallino - központ 100m.')"
				},
				new Building
				{
					Name = "Hotel Veneto",
					CityId = 2,
					SeaDistance = 2000,
					Shore = ShoreType.Sandy,
					Features = Feature.CoastService,
					LocationX = 45.5171,
					LocationY = 12.6694,
					Comment = "Szálloda községben, 2km strandtól. A hotelban van étkezde (csak reggeli), kerékpárkölcsönző. A szállodában nincs étterem, individuális étkezés étteremben 500m a hoteltől. Internet-csatlakozás Wi-Fi és légkondicionáló illeték fejében. Parkoló - nem őrzött, ingyenes. Busz 500m. Vonat (állomás Venezia) 25km. Lido di Jesolo - központ 6km.')"
				},
			};

			foreach (Building b in buildings)
			{
				_context.Buildings.Add(b);
			}

			_context.SaveChanges();
		}

		/// <summary>
		/// Apartmanok inicializálása.
		/// </summary>
		private static void SeedApartments()
		{
			var apartments = new Apartment[]
			{
				new Apartment
				{
					BuildingId = 1,
					Room = 1,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverő), fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok.",
					Price = 21
				},
				new Apartment
				{
					BuildingId = 1,
					Room = 2,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverő), fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok.",
					Price = 21
				},
				new Apartment
				{
					BuildingId = 1,
					Room = 3,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverő), fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok.",
					Price = 21
				},
				new Apartment
				{
					BuildingId = 1,
					Room = 4,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverő), fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok",
					Price = 21
				},
				new Apartment
				{
					BuildingId = 2,
					Room = 1,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, TV, nappali két pótággyal, fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok",
					Price = 28
				},
				new Apartment
				{
					BuildingId = 2,
					Room = 2,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, TV, nappali két pótággyal, fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok",
					Price = 28
				},
				new Apartment
				{
					BuildingId = 2,
					Room = 3,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, TV, nappali két pótággyal, fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok",
					Price = 28
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 1,
					Turnday = DayOfWeek.Sunday,
					Comment = "1-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 18
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 2,
					Turnday = DayOfWeek.Sunday,
					Comment = "1-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 18
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 3,
					Turnday = DayOfWeek.Sunday,
					Comment = "1-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 18
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 4,
					Turnday = DayOfWeek.Sunday,
					Comment = "2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 32
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 5,
					Turnday = DayOfWeek.Sunday,
					Comment = "2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 32
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 6,
					Turnday = DayOfWeek.Sunday,
					Comment = "2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 32
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 7,
					Turnday = DayOfWeek.Sunday,
					Comment = "2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 32
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 8,
					Turnday = DayOfWeek.Sunday,
					Comment = "2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 32
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 9,
					Turnday = DayOfWeek.Sunday,
					Comment = "2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, hajszárító, telefon, erkély, műholdas TV",
					Price = 32
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 10,
					Turnday = DayOfWeek.Sunday,
					Comment = "2x2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, telefon, erkély, műholdas TV, nappali, kis konyha",
					Price = 54
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 11,
					Turnday = DayOfWeek.Sunday,
					Comment = "2x2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, telefon, erkély, műholdas TV, nappali, kis konyha",
					Price = 54
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 12,
					Turnday = DayOfWeek.Sunday,
					Comment = "2x2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, telefon, erkély, műholdas TV, nappali, kis konyha",
					Price = 54
				},
				new Apartment
				{
					BuildingId = 3,
					Room = 13,
					Turnday = DayOfWeek.Sunday,
					Comment = "2x2-ágyas szoba, fürdőszoba zuhanyozófülkével és WC-vel, telefon, erkély, műholdas TV, nappali, kis konyha",
					Price = 54
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 1,
					Turnday = DayOfWeek.Saturday,
					Comment = "3-ágyas szoba lehetőség pótágyra, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 65
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 2,
					Turnday = DayOfWeek.Saturday,
					Comment = "2-ágyas szoba két pótággyal, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 42
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 3,
					Turnday = DayOfWeek.Saturday,
					Comment = "2-ágyas szoba két pótággyal, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 42
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 4,
					Turnday = DayOfWeek.Saturday,
					Comment = "2-ágyas szoba két pótággyal, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 42
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 5,
					Turnday = DayOfWeek.Saturday,
					Comment = "2-ágyas szoba két pótággyal, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 42
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 6,
					Turnday = DayOfWeek.Saturday,
					Comment = "2-ágyas szoba, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 34
				},
				new Apartment
				{
					BuildingId = 4,
					Room = 7,
					Turnday = DayOfWeek.Saturday,
					Comment = "2-ágyas szoba, TV, fürdőszoba (zuhanyozófülke, WC, bidé), erkély",
					Price = 34
				},
			};
			foreach (Apartment a in apartments)
			{
				_context.Apartments.Add(a);
			}

			_context.SaveChanges();
		}

		/// <summary>
		/// Képek inicializálása.
		/// </summary>
		/// <param name="imageDirectory">Képek könyvtára.</param>
		private static void SeedBuildingImages(string imageDirectory)
		{
			// Ellenőrizzük, hogy képek könyvtára létezik-e.
			if (Directory.Exists(imageDirectory))
			{
				var images = new List<BuildingImage>();

				// Képek aszinkron betöltése.
				var largePath = Path.Combine(imageDirectory, "petra_1.png");
				var smallPath = Path.Combine(imageDirectory, "petra_1_thumb.png");
				if (File.Exists(largePath) && File.Exists(smallPath))
				{
					images.Add(new BuildingImage
					{
						BuildingId = 1,
						ImageLarge = File.ReadAllBytes(largePath),
						ImageSmall = File.ReadAllBytes(smallPath)
					});
				}

				largePath = Path.Combine(imageDirectory, "petra_2.png");
				smallPath = Path.Combine(imageDirectory, "petra_2_thumb.png");
				if (File.Exists(largePath) && File.Exists(smallPath))
				{
					images.Add(new BuildingImage
					{
						BuildingId = 1,
						ImageLarge = File.ReadAllBytes(largePath),
						ImageSmall = File.ReadAllBytes(smallPath)
					});
				}

				largePath = Path.Combine(imageDirectory, "cavallino_1.png");
				smallPath = Path.Combine(imageDirectory, "cavallino_1_thumb.png");
				if (File.Exists(largePath) && File.Exists(smallPath))
				{
					images.Add(new BuildingImage
					{
						BuildingId = 3,
						ImageLarge = File.ReadAllBytes(largePath),
						ImageSmall = File.ReadAllBytes(smallPath)
					});
				}

				foreach (var image in images)
				{
					_context.BuildingImages.Add(image);
				}

				_context.SaveChanges();
			}
		}
	}
}
