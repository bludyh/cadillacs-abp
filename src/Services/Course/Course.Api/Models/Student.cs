using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
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
        // TODO: Discuss with group about Class Group and Student.
        public List<StudentGroup> StudentGroups { get; set; }
    }
}
