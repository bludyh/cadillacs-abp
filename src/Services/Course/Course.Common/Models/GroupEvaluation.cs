namespace Course.Common.Models
{
    public class GroupEvaluation
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

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
