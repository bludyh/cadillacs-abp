using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class Requirement
    {
        public Course Course { get; set; }
        public List<Course> RequiredCourses { get; set; }
    }
}
