using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class AssignmentAttachmentReadDto
    {
        public AssignmentReadDto Assignment { get; set; }
        public AttachmentReadDto Attachment { get; set; }
    }
}
