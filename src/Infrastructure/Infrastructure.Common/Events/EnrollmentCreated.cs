﻿using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class EnrollmentCreated : Event
    {
        public int StudentId { get; set; }
        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }
        public double FinalGrade { get; set; }
    }
}
