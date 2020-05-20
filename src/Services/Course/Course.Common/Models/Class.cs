﻿using System;
using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Class
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<ClassSchedule> ClassSchedules { get; set; }
        public List<StudyMaterial> StudyMaterials { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Group> Groups { get; set; }
        public List<Lecturer> Lecturers { get; set; }
    }
}