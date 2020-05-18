using Pitstop.Infrastructure.Messaging;
using System;

namespace Infrastructure.Common.Events
{
    public class ClassCreated : Event
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
