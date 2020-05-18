using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Persistence;

namespace TodoList.WebApi.Tests
{
    public static class TestDbInitializer
    {
        public static void Initialize(TodoListContext context)
        {
            IList<List> defaultLists = new List<List>
            {
                new List
                {
                    Id = 1,
                    Name = "Bevásárlás",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Alma",
                            Deadline = DateTime.Now.AddDays(1)
                        },
                        new Item()
                        {
                            Name = "Körte",
                            Deadline = DateTime.Now.AddDays(1)
                        },
                        new Item()
                        {
                            Name = "Sör",
                            Deadline = DateTime.Now,
                        }
                    }
                },
                new List
                {
                    Id = 2,
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
                },
                new List
                {
                    Id = 3,
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
                }
            };

            foreach (List list in defaultLists)
                context.Lists.Add(list);

            context.SaveChanges();
        }
    }
}
