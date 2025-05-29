using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using Microsoft.EntityFrameworkCore;

namespace Lab2._2
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
                var courses = db.Courses.ToList();
                db.Teachers.Find(1).Courses.AddRange(courses.GetRange(0, 2));
                db.Teachers.Find(2).Courses.AddRange(courses.GetRange(2, 2));

                int maxi = db.Students.Count();
                for (int i = 1; i <= maxi; i++)
                {
                    Student student = db.Students.Find(i);
                    student.Courses.Add(courses[i - 1]);
                    student.Courses.Add(courses[(i > maxi - 1 ? 0 : i)]);
                }
                db.SaveChanges();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                //Ленивая
                Console.WriteLine("Lazy load");
                var result = db.Teachers
                    .OrderBy(x => x.Id)
                    .ToList();
                foreach (var teacher in result)
                {
                    Console.WriteLine($"{teacher.Id}. {teacher.Name} students:");
                    foreach (var course in teacher.Courses)
                    {
                        Console.WriteLine($"\t{course.Title}, Duration {course.Duration} students:");
                        foreach (var student in course.Students)
                        {
                            Console.WriteLine($"\t\t{student.Id}.{student.Name}, Age {student.Age}");
                        }

                    }
                }

                //Явная
                Console.WriteLine("explicit load");
                var result2 = db.Teachers
                    .OrderBy(x => x.Id)
                    .ToList();
                foreach (var teacher in result2)
                {
                    Console.WriteLine($"{teacher.Id}. {teacher.Name} students:");
                    db.Entry(teacher).Collection(t => t.Courses).Load();
                    foreach (var course in teacher.Courses)
                    {
                        Console.WriteLine($"\t{course.Title}, Duration {course.Duration} students:");
                        db.Entry(course).Collection(c => c.Students).Load();
                        foreach (var student in course.Students)
                        {
                            Console.WriteLine($"\t\t{student.Id}.{student.Name}, Age {student.Age}");
                        }

                    }
                }
                //Жадная
                Console.WriteLine("eager load");
                var result3 = db.Teachers
                    .Include(t => t.Courses)
                    .ThenInclude(c => c.Students)
                    .OrderBy(x => x.Id)
                ;
                foreach (var teacher in result3)
                {
                    Console.WriteLine($"{teacher.Id}. {teacher.Name} students:");
                    foreach (var course in teacher.Courses)
                    {
                        Console.WriteLine($"\t{course.Title}, Duration {course.Duration} students:");
                        foreach (var student in course.Students)
                        {
                            Console.WriteLine($"\t\t{student.Id}.{student.Name}, Age {student.Age}");
                        }

                    }
                }

            }
        }
    }
}
