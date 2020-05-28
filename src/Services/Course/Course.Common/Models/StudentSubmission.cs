namespace Course.Common.Models
{
    public class StudentSubmission : Submission
    {

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
