using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Enrollment> Enrollments { get; set; }
        public List<StudentSubmission> StudentSubmissions { get; set; }
    }
}
