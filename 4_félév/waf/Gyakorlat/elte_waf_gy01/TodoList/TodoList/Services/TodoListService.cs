using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TodoList.Model;

namespace TodoList.Services
{
    public class TodoListService
    {
        private readonly TodoListContext _context;
        public TodoListService(TodoListContext context)
        {
            _context = context;
        }

        public List<List> GetLists(String name = null)
        {
            return _context.Lists
                .Where(l => l.Name.Contains(name ?? ""))
                .OrderBy(l => l.Name)
                .ToList();
        }

        public List GetListByID(int id)
        {
            return _context.Lists
                .Include(l => l.Items)
                .Single(l => l.ID == id); // throws exception if id not found
        }

        public List<Item> GetItemsByListID(int id)
        {
            return _context.Lists
                .Include(l => l.Items)
                .Single(l => l.ID == id) // throws exception if id not found
                .Items.ToList();
        }

        public void AddItemToList(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "The item to add must not be null.");

            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void ChangeListName(int id, String newName)
        {
            _context.Lists
                .Single(l => l.ID == id) // throws exception if id not found
                .Name = newName;
            
            _context.SaveChanges();
        }

        public void RemoveItemByName(int id, String name)
        {
            Item item = _context.Items
                .Where(i => i.ListId == id)
                .FirstOrDefault(i => i.Name == name);
            
            _context.Items.Remove(item);
            _context.SaveChanges();
        }
    }
}
