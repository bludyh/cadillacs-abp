using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos
{
    public class TeacherMentorReadDto
    {
        public string MentorType { get; set; }
        public StudentReadDto Student { get; set; }
    }
}
