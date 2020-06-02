using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class EnrollmentReadDto
    {
        public StudentReadDto Student { get; set; }
        
        public ClassReadDto Class { get; set; }

        public GroupReadDto Group { get; set; }

        public double FinalGrade { get; set; }
    }
}
