using System;
using System.Data.Entity;

namespace ELTE.Windows.TodoList.Model
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<List> Lists { get; set; }

        public DbSet<Item> Items { get; set; }
    }
}