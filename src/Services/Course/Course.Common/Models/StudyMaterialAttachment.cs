namespace Course.Common.Models
{
    public class StudyMaterialAttachment
    {
        public int StudyMaterialId { get; set; }
        public StudyMaterial StudyMaterial { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
