using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Model
{
    public static class DbInitializer
    {
        public static void Initialize(TodoListContext context)
        {
            // Create database if not exists based on current code-first model.
            //context.Database.EnsureCreated();

            // Create / update database based on migration classes.
            context.Database.Migrate();
            // Use only one of the above!

            if (context.Lists.Any())
            {
                return;
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
                            Deadline = DateTime.Now
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
                            Name = "MVC beadandó",
                            Description = "Nem lesz nehéz.",
                            Deadline = DateTime.Now.AddDays(7)
                        },
                        new Item()
                        {
                            Name = "WebAPI beadandó",
                            Description = "Ez még kevésbé lesz nehéz.",
                            Deadline = DateTime.Now.AddDays(10)
                        }
                    }
                }
            };

            foreach (var list in defaultLists)
            {
                context.Lists.Add(list);
            }

            context.SaveChanges();
        }
    }
}
