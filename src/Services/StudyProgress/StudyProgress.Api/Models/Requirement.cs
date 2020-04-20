using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class Requirement
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int RequiredCourseId { get; set; }
        public Course RequiredCourse { get; set; }
    }
}
