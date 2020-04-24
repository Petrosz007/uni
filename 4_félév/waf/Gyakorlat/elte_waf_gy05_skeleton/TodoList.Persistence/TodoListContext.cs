using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Persistence
{
    public class TodoListContext : IdentityDbContext<ApplicationUser>
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
            : base(options)
        {
        }

        public DbSet<List> Lists { get; set; }

        public DbSet<Item> Items { get; set; }
    }
}