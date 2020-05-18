namespace StudyProgress.Common.Models
{
    public class ProgramCourse
    {
        public string ProgramId { get; set; }
        public Program Program { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
