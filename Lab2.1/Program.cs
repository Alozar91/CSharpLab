using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;

namespace Lab2._1
{
    internal class Program
    {
        internal static IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Courses.AddRange(
                    new Course { Title = "C# Language", Duration = 40, Description = "C# Language" },
                    new Course { Title = ".Net Client-Server", Duration = 40, Description = "Creating client server for .NET using C#" },
                    new Course { Title = "Pattern", Duration = 24, Description = "OOP Pattern" },
                    new Course { Title = "JavaScript", Duration = 24, Description = "JavaScript for web developers" }
                    );
                db.SaveChanges();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Course c = db.Courses.Where(x => x.Title == "C# Language").FirstOrDefault();
                if (c != null)
                {
                    c.Duration= 6;
                    db.SaveChanges();
                }
                var courceList = db.Courses.ToList();
                Console.WriteLine("Course list:");
                foreach (var course in courceList)
                {
                    Console.WriteLine($"{course.id}. {course.Title}, {course.Description} with duration {course.Duration}");
                }
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Course c = new Course() { Title = "ABV", Description = "Course ABV", Duration = 10 };
                db.Courses.Add(c);
                db.SaveChanges();
                var courceList = db.Courses.ToList();
                Console.WriteLine("Course list:");
                foreach (var course in courceList)
                {
                    Console.WriteLine($"{course.id}. {course.Title}, {course.Description} with duration {course.Duration}");
                }
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Course c = db.Courses.Where(x => x.Title == "Pattern").FirstOrDefault();
                if (c != null)
                {
                    db.Courses.Remove(c);
                    db.SaveChanges();
                }
                var courceList = db.Courses.OrderBy(c=> c.Title).ToList();
                Console.WriteLine("Course list:");
                foreach (var course in courceList)
                {
                    Console.WriteLine($"{course.id}. {course.Title}, {course.Description} with duration {course.Duration}");
                }
            }
        }
    }
}
