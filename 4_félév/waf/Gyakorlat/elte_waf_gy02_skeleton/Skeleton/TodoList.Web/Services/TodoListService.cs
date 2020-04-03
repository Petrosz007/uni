using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Web.Models;

namespace TodoList.Web.Services
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


        public List GetListById(int id)
        {
            return _context.Lists
                .Include(l => l.Items)
                .Single(l => l.Id == id); // throws exception if id not found
        }
    }
}
