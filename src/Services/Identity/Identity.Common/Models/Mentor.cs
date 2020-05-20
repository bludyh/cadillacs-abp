namespace Identity.Common.Models
{
    public class Mentor
    {

        public MentorType MentorType { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
