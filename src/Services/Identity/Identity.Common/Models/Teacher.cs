using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class Teacher : Employee
    {

        public List<Mentor> Mentors { get; set; }

    }
}
