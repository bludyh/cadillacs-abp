using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class AttachmentReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
    }
}
