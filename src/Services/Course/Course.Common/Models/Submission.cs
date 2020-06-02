namespace Course.Common.Models
{
    public class Submission
    {

        public int Id { get; set; }

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }

        public double? Grade { get; set; }
    }
}
