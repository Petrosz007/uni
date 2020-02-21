using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ELTE.TravelAgency.Web.Models
{
	public static class DbInitializer
    {
	    public static void Initialize(IServiceProvider serviceProvider)
		{
		    TravelAgencyContext context = serviceProvider.GetRequiredService<TravelAgencyContext>();

            // Adatbázis létrehozása, amennyiben nem létezik
            context.Database.EnsureCreated();

			// Apartmanok keresése
			if (context.Apartments.Any())
			{
				return; // Az adatbázis már inicializálva van.
			}

			var cities = new City[]
			{
				new City { Name = "Cavallino" },
				new City { Name = "Lido di Jesolo" },
			};
			foreach (City c in cities)
			{
				context.Cities.Add(c);
			}
			context.SaveChanges();

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
				context.Buildings.Add(b);
			}
			context.SaveChanges();

			var apartments = new Apartment[]
			{
				new Apartment
				{
					BuildingId =  1,
					Room = 1,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverő), fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok.",
					Price = 21
				},
				new Apartment
				{
					BuildingId =  1,
					Room = 2,
					Turnday = DayOfWeek.Saturday,
					Comment = "2x2-ágyas szoba, nappali két pótággyal (szétnyitható heverő), fürdőszoba zuhanyozófülkével és WC-vel, konyha (gáztűzhely, hűtőszekrény), étkezősarok.",
					Price = 21
				}
			};
			foreach (Apartment a in apartments)
			{
				context.Apartments.Add(a);
			}
			context.SaveChanges();
		}
	}
}
