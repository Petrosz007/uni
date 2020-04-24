using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Persistence.Services
{
    public class TodoListService
    {
        private readonly TodoListContext _context;

        public TodoListService(TodoListContext context)
        {
            _context = context;
        }

        #region List

        public List<List> GetLists(String name = null)
        {
            return _context.Lists
                .Where(l => l.Name.Contains(name ?? ""))
                .OrderBy(l => l.Name)
                .ToList();
        }

        public List GetListById(int id)
        {
            return _context.Lists
                .Include(l => l.Items)
                .Single(l => l.Id == id); // throws exception if id not found
        }

        public bool CreateList(List list)
        {
            try
            {
                _context.Add(list);
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool UpdateList(List list)
        {
            try
            {
                _context.Update(list);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteList(int id)
        {
            var list = _context.Lists.Find(id);
            if (list == null)
                return false;

            try
            {
                _context.Remove(list);
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Item

        public List<Item> GetItems()
        {
            return _context.Items
                .OrderBy(i => i.Name)
                .ToList();
        }

        public Item GetItem(int id)
        {
            return _context.Items
                .Include(i => i.List)
                .FirstOrDefault(i => i.Id == id);
        }

        public bool CreateItem(Item item)
        {
            try
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool UpdateItem(Item item)
        {
            try
            {
                _context.Update(item);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                return false;
            }
            catch(DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool UpdateItemExcludeImage(Item item)
        {
            try
            {
                _context.Update(item);
                _context.Entry(item).Property("Image").IsModified = false;
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                return false;
            }
            catch(DbUpdateException)
            {
                return false;
            }

            return true;
        }

        public bool DeleteItem(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
                return false;

            try
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
