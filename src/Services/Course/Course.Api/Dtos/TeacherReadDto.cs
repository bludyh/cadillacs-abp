using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class TeacherReadDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
