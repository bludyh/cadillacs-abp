using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class Student : User
    {
        public List<Enrollment> Enrollments { get; set; }
    }
}
