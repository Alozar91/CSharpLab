using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3._1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .ToTable(nameof(Course))
                .HasData(
                    new Course { Id = 1, Title = "C# Language", Duration = 40, Description = "C# Language" },
                    new Course { Id = 2, Title = ".Net Client-Server", Duration = 40, Description = "Creating client server for .NET using C#" },
                    new Course { Id = 3, Title = "Pattern", Duration = 24, Description = "OOP Pattern" },
                    new Course { Id = 4, Title = "JavaScript", Duration = 24, Description = "JavaScript for web developers" }
                );

            modelBuilder.Entity<Teacher>()
                .ToTable(nameof(Teacher))
                .HasData(
                    new Teacher() { Id = 1, Name = "Sergey" },
                    new Teacher() { Id = 2, Name = "Fedor" }
                );
            modelBuilder.Entity<Student>()
                .ToTable(nameof(Student))
                .HasData(
                    new Student() { Id = 1, Name = "Anna", Age = 23 },
                    new Student() { Id = 2, Name = "Alexey", Age = 32 },
                    new Student() { Id = 3, Name = "Andrey", Age = 17 },
                    new Student() { Id = 4, Name = "Helen", Age = 32 }
                );

            //modelBuilder.Entity<Person>()
            //    .HasMany(p => p.Countries)
            //    .WithMany(c => c.People)
            //    .UsingEntity<Citizen>();
            //modelBuilder.Entity<Course>()
            //    .HasMany(p => p.Teachers)
            //    .WithMany(t => t.Courses)
            //    .UsingEntity(t => t.ToTable("CourseTeacher"));
            //modelBuilder.Entity<Student>()
            //    .HasMany(p => p.Courses)
            //    .WithMany(t => t.Students)
            //    .UsingEntity(t=> t.ToTable("CourseStudent"));
        }
    }
}
