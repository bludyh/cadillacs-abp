using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class SubmissionReadDto
    {
        public int Id { get; set; }

        public AssignmentReadDto Assignment { get; set; }
        public AttachmentReadDto Attachment { get; set; }
        public double? Grade { get; set; }
    }
}
