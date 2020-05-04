using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Dtos
{
    public class ClassEnrollmentReadDto
    {
        public StudentReadDto Student { get; set; }

        public double FinalGrade { get; set; }
    }
}
