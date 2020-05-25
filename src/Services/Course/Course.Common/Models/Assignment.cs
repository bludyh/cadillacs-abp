﻿using System;

namespace Course.Common.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }
        public Class Class { get; set; }

        public AssignmentType Type { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        public int Weight { get; set; }
    }
}
