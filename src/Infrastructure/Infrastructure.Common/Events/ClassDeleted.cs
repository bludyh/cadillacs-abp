using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class ClassDeleted : Event
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string CourseId { get; set; }
    }
}
