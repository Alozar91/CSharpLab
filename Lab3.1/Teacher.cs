

namespace Lab3._1
{
    public class Teacher
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public virtual List<Course> Courses { get; set; } = new();
    }
}
