using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models {
    public class Teacher : Employee {

        public List<Mentor> Mentors { get; set; }

    }
}
