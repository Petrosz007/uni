using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TodoList.Model;
using TodoList.Services;

namespace TodoList
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TodoListContext())
            {
                DbInitializer.Initialize(context);
                TodoListService service = new TodoListService(context);

                // Get all lists
                Console.WriteLine("Current lists:");
                foreach (var list in service.GetLists())
                {
                    Console.WriteLine(list.Name);
                }

                // Get items in list with id 1
                int id = 1;
                Console.WriteLine();
                Console.WriteLine("Current items in List no. " + id + ":");
                foreach (var item in service.GetItemsByListID(id))
                {
                    Console.WriteLine(item.Name);
                }

                // Add new item to list no.1
                Item it = new Item
                {
                    Name = "Csoki",
                    Deadline = DateTime.Now.AddDays(2),
                    ListId = 1
                };

                service.AddItemToList(it);

                // Get items in list with id 1
                Console.WriteLine();
                Console.WriteLine("Current items in List no. " + id + ":");
                foreach (var item in service.GetItemsByListID(1))
                {
                    Console.WriteLine(item.Name);
                }

                // Rename list with id 1
                service.ChangeListName(1, "Shopping list");

                // Get all lists
                Console.WriteLine();
                Console.WriteLine("Current lists:");
                foreach (var list in service.GetLists())
                {
                    Console.WriteLine(list.Name);
                }

                // Remove item from list no.1
                service.RemoveItemByName(id, "Csoki");

                // Get items in list with id 1
                Console.WriteLine();
                Console.WriteLine("Current items in List no. " + id + ":");
                foreach (var item in service.GetItemsByListID(1))
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
    }
}
