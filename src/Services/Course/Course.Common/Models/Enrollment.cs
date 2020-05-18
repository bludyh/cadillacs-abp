namespace Course.Common.Models
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public int ClassCourseId { get; set; }
        public Class Class { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public double FinalGrade { get; set; }
    }
}
