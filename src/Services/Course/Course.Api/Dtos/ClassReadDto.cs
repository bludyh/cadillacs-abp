using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class ClassReadDto
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public CourseReadDto Course { get; set; }
    }
}
