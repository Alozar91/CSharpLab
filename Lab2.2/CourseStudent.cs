using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2._2
{
    public class CourseStudent
    {
        public int Id { get; set; }
        public virtual int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
