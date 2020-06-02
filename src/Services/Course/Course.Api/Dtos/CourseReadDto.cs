using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class CourseReadDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Credit { get; set; }
    }
}
