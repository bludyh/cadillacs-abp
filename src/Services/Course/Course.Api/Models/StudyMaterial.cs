using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
{
    public class StudyMaterial
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Class Class { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }

    }
}
