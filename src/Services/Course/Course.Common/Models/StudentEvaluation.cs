namespace Course.Common.Models
{
    public class StudentEvaluation
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
    }
}
