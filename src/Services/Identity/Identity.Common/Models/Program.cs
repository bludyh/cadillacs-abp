using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class Program
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public string SchoolId { get; set; }
        public School School { get; set; }

        public List<Student> Students { get; set; }
        public List<EmployeeProgram> EmployeePrograms { get; set; }

    }
}
