namespace StudyProgress.Common.Models
{
    public class Requirement
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string RequiredCourseId { get; set; }
        public Course RequiredCourse { get; set; }
    }
}
