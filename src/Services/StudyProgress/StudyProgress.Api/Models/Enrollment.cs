using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public double FinalGrade { get; set; }
    }
}
