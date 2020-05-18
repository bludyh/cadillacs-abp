namespace Course.Common.Models
{
    public class Lecturer
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public int ClassCourseId { get; set; }
        public Class Class { get; set; }
    }
}
