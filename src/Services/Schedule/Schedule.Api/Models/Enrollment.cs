using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public int Semester { get; set; }

        public int Year { get; set; }

        public int CourseId { get; set; }

        public Student Student { get; set; }

        public Class Class { get; set; }
    }
}
