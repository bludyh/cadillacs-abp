using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class StudyMaterialAttachmentReadDto
    {
        public int StudyMaterialId { get; set; }
        public int AttachmentId { get; set; }
    }
}
