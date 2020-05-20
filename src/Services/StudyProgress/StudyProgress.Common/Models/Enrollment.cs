namespace StudyProgress.Common.Models
{
    public class Enrollment
    {
        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }
        public Class Class { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public double FinalGrade { get; set; }
    }
}
