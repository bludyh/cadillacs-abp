using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Dtos
{
    public class EnrollmentReadDto
    {
        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }
        public int StudentId { get; set; }

        public double FinalGrade { get; set; }
    }
}
