using System.Collections.Generic;

namespace StudyProgress.Common.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }

        public List<Enrollment> Enrollments { get; set; }
    }
}
