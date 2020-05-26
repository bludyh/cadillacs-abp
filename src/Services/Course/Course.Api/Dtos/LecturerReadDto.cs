using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class LecturerReadDto
    {
        public TeacherReadDto Teacher { get; set; }
        public ClassReadDto Class { get; set; }
    }
}
