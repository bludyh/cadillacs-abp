using Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos
{
    public class StudentMentorReadDto
    {
        public string MentorType { get; set; }
        public EmployeeReadDto Teacher { get; set; }
    }
}
