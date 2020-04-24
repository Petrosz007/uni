using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoList.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, string imageDirectory)
        {
            TodoListContext context = serviceProvider.GetRequiredService<TodoListContext>();

            /*
             Adatbázis létrehozása (amennyiben nem létezik), illetve a migrációk alapján a frissítése.
             Amennyiben teljesen el szeretnénk vetni a jelenlegi adatbázisunk, akkor a
             törléshez az Sql Server Object Explorer ablak használható a Visual Studioban.
             Itt SQL Server > (localdb)\\MSSQLLocalDB útvonalon találjuk az adatbázisainkat.
             */
            //context.Database.EnsureDeleted();
            context.Database.Migrate();

            // Listák keresése
            if (context.Lists.Any())
            {
                return; // Az adatbázis már inicializálva van.
            }

            IList<List> defaultLists = new List<List>();

            var applePath = Path.Combine(imageDirectory, "apple.png");
            var pearPath = Path.Combine(imageDirectory, "pear.png");
            var beerPath = Path.Combine(imageDirectory, "beer.png");

            defaultLists.Add(new List
            {
                Name = "Bevásárlás",
                Items = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Alma",
                        Deadline = DateTime.Now.AddDays(1),
                        Image = File.Exists(applePath) ? File.ReadAllBytes(applePath) : null
                    },
                    new Item()
                    {
                        Name = "Körte",
                        Deadline = DateTime.Now.AddDays(1),
                        Image = File.Exists(pearPath) ? File.ReadAllBytes(pearPath) : null
                    },
                    new Item()
                    {
                        Name = "Sör",
                        Deadline = DateTime.Now,
                        Image = File.Exists(beerPath) ? File.ReadAllBytes(beerPath) : null
                    }
                }
            });

            defaultLists.Add(
                new List
                {
                    Name = "Beadandók",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "WAF beadandó",
                            Deadline = DateTime.Now.AddDays(7)
                        },
                        new Item()
                        {
                            Name = "MI beadandó",
                            Deadline = DateTime.Now.AddDays(10)
                        }
                    }
                });

            defaultLists.Add(
                new List
                {
                    Name = "Fejlesztés",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Felhasználókezelés",
                            Description = "Bejelentkezés és kijelentkezés megvalósítása süti alapú authentikációval.",
                            Deadline = DateTime.Now.AddDays(4)
                        },
                        new Item()
                        {
                            Name = "Validáció",
                            Description =
                                "A nézetmodell validációs annotációkkal történő ellátása és a szabályok teljesülésének ellenőrzése.",
                            Deadline = DateTime.Now.AddHours(4)
                        },
                        new Item()
                        {
                            Name = "Regisztráció",
                            Description = "Regisztráció implementációja email cím megerősítéssel.",
                            Deadline = DateTime.Now.AddDays(2)
                        },
                        new Item()
                        {
                            Name = "Asztali kliens",
                            Description = "Asztali WPF adminisztrációs kliens fejlesztése.",
                            Deadline = DateTime.Now.AddDays(12)
                        }
                    }
                });

            foreach (List list in defaultLists)
                context.Lists.Add(list);

            context.SaveChanges();
        }
    }
}