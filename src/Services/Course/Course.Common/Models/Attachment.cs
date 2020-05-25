using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

        public List<StudentEvaluation> StudentEvaluations { get; set; }
        public List<StudyMaterialAttachment> StudyMaterialAttachments { get; set; }
        public List<AssignmentAttachment> AssignmentAttachments { get; set; }
        public List<GroupEvaluation> GroupEvaluations { get; set; }
    }
}
