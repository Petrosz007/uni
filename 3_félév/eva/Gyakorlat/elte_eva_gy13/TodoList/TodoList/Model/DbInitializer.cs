using System;
using System.Collections.Generic;
using System.Linq;

namespace ELTE.Windows.TodoList.Model
{
    public static class DbInitializer
    {
        public static void Initialize(TodoListDbContext context)
        {
            context.Database.CreateIfNotExists();

            // Listák keresése
            if (context.Lists.Any())
            {
                return; // Az adatbázis már inicializálva van adatokkal.
            }

            IList<List> defaultLists = new List<List>
            {
                new List
                {
                    Name = "Bevásárlás",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Kenyér",
                            Deadline = DateTime.Now.AddDays(1),
                            Description = "Fél kiló"
                        },
                        new Item()
                        {
                            Name = "Tej",
                            Deadline = DateTime.Now.AddDays(1),
                            Description = "1 liter"
                        },
                        new Item()
                        {
                            Name = "Sör",
                            Deadline = DateTime.Now,
                            Description = "6 doboz"
                        }
                    }
                },
                new List
                {
                    Name = "Beadandók",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "EVA beadandó",
                            Deadline = DateTime.Now.AddDays(7),
                            Description = "WPF beadandó + EF perzisztencia"
                        },
                        new Item()
                        {
                            Name = "Operációs rendszerek beadandó",
                            Deadline = DateTime.Now.AddDays(10),
                            Description = "Zöld-fehér legyen"
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