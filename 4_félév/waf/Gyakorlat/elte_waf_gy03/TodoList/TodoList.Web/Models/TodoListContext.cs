using System;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Web.Models
{
    public class TodoListContext : DbContext
    { 
        public TodoListContext(DbContextOptions<TodoListContext> options)
            : base(options)
        {
        }

        public DbSet<List> Lists { get; set; }

        public DbSet<Item> Items { get; set; }
    }
}