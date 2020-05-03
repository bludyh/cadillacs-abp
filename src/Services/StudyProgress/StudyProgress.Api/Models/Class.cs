using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class Class
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
