using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class Lecturer
    {
        public int TeacherId { get; set; }

        public int ClassId { get; set; }

        public int Semester { get; set; }

        public int Year { get; set; }

        public int CourseId { get; set; }

        public Teacher Teacher { get; set; }

        public Class Class { get; set; }
    }
}
