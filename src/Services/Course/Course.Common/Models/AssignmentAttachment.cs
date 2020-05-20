namespace Course.Common.Models
{
    public class AssignmentAttachment
    {
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
