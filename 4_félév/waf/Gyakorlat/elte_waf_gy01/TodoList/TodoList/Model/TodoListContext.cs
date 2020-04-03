using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TodoList.Model
{
    public class TodoListContext : DbContext
    {
        public DbSet<List> Lists { get; set; }

        public DbSet<Item> Items { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = configBuilder.Build();

            DbType dbType = configuration.GetValue<DbType>("DbType");
            // Need Microsoft.EntityFrameworkCore.Tools package for add-migration command
            switch (dbType)
            {
                // Need Microsoft.EntityFrameworkCore.SqlServer package for this
                case DbType.SqlServer:
                    optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
                    break;
                // Need Microsoft.EntityFrameworkCore.Sqlite package for this
                // Using Microsoft.EntityFrameworkCore.Sqlite.Core causes error with update-database
                case DbType.Sqlite:
                    optionsBuilder.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
                    break;
            }
        }
    }
}
