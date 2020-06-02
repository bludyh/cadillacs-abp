using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class StudyMaterialAttachmentReadDto
    {
        public StudyMaterialReadDto StudyMaterial { get; set; }
        public AttachmentReadDto Attachment { get; set; }
    }
}
