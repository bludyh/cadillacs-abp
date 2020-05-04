﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Models
{
    public class Program
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalCredit { get; set; }

        public string SchoolId { get; set; }
        public School School { get; set; }
        public List<ProgramCourse> ProgramCourses { get; set; }
    }
}
