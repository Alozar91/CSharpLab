using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2._2
{
    public class CourseTeacher
    {
        public int Id { get; set; }
        public virtual int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
