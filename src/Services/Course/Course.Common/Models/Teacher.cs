using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Should be abbreviation?
        public string Initials { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Lecturer> Lecturers { get; set; }
    }
}
