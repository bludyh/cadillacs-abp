using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }
        public Class Class { get; set; }
        public string Name { get; set; }
        public int MaxSize { get; set; }


        public List<Enrollment> Enrollments { get; set; }
    }
}
