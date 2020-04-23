using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
{
    public class StudyMaterialAttachment
    {
        public int StudyMaterialId { get; set; }
        public StudyMaterial StudyMaterial { get; set; }

        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
