using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class Teacher : User
    {

        public List<Lecturer> Lecturers { get; set; }


    }
}
