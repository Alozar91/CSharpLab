using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._2
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
   : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            modelBuilder.Entity<Person>().HasData(
                new Person() { Id = 1, Name = "Sergey", Age = 33 },
                new Person() { Id = 2, Name = "Fedor", Age = 43 }
            );
        }
    }
}
