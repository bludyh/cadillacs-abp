using System.Collections.Generic;

namespace Course.Common.Models
{
    public class StudyMaterial
    {
        public int Id { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public int ClassCourseId { get; set; }
        public Class Class { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Week { get; set; }

        public List<StudyMaterialAttachment> StudyMaterialAttachments { get; set; }
    }
}
