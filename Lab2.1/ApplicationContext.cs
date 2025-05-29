using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2._1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = Program.config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.LogTo(Console.WriteLine,LogLevel.Information);
            //optionsBuilder.LogTo(s=>Debug.WriteLine(s));

        }
    }
}
