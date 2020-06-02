using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class StudentSubmissionReadDto : SubmissionReadDto
    {
        public StudentReadDto Student { get; set; }
    }
}
