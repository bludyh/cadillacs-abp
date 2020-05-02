using System;
using StudyProgress.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Dtos
{
    public class StudentEnrollmentReadDto
    {
        //public StudentDto Student { get; set; }

        public ClassDto Class { get; set; }

        public double FinalGrade { get; set; }


    }
}
