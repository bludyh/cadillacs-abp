using System;
using System.Collections.Generic;

namespace Schedule.Common.Models
{
    public class Class
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string CourseId { get; set; }

        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }
    }
}
