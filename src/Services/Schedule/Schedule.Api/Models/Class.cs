using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class Class
    {
        public int Id { get; set; }

        public int Semester { get; set; }

        public int Year { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Enrollment> Enrollments { get; set; }

        public List<Lecturer> Lecturers { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }


    }
}
