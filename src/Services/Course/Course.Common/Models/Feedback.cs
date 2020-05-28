namespace Course.Common.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string Description { get; set; }
    }
}
