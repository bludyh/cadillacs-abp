using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credit { get; set; }

        //public List<Requirement> Requirements { get; set; }
        public List<ProgramCourse> ProgramCourses { get; set; }
        public List<Class> Classes { get; set; }
    }
}
