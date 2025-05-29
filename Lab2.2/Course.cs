using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2._2
{
    public class Course
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }
        public virtual List<Teacher> Teachers { get; set; } = new();
        public virtual List<Student> Students { get; set; } = new();
    }
}
