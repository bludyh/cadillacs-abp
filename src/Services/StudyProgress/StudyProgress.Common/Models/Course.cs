using System.Collections.Generic;

namespace StudyProgress.Common.Models
{
    public class Course
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credit { get; set; }

        public List<Requirement> Requirements { get; set; }
        public List<ProgramCourse> ProgramCourses { get; set; }
        public List<Class> Classes { get; set; }
    }
}
